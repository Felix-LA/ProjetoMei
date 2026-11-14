<?php
/**
 * Configuração central do front + helpers de comunicação com a API (.NET).
 * Todas as páginas dão require neste arquivo.
 */

session_start();

// ---- URL da API (ajuste a porta se necessário; ver launchSettings.json) ----
define('API_BASE', 'http://localhost:5037/api/UsuarioMei');

// ---- Caminhos do front ----
// BASE_URL: endereço (no navegador) da pasta View.
// Se o projeto estiver em http://localhost/ProjetoMei/View, deixe '/ProjetoMei/View'.
define('BASE_URL', '/ProjetoMei/View');

// ASSETS_URL: endereço da pasta assets (css, js, styles, images, fonts).
define('ASSETS_URL', BASE_URL . '/assets');

// VIEW_PATH: caminho da pasta View no disco, usado nos require/include do PHP.
define('VIEW_PATH', dirname(__DIR__));

// Mapa de rotas (nome => arquivo dentro de View).
$GLOBALS['ROTAS'] = require __DIR__ . '/rotas.php';

/**
 * Faz uma requisição para a API.
 *
 * @return array ['status' => int HTTP, 'dados' => mixed JSON decodificado, 'erro' => ?string]
 */
function api(string $metodo, string $caminho = '', ?array $corpo = null): array
{
    $ch = curl_init(API_BASE . $caminho);

    $headers = ['Accept: application/json'];
    $opcoes = [
        CURLOPT_CUSTOMREQUEST  => strtoupper($metodo),
        CURLOPT_RETURNTRANSFER => true,
        CURLOPT_TIMEOUT        => 10,
    ];

    if ($corpo !== null) {
        $headers[] = 'Content-Type: application/json';
        $opcoes[CURLOPT_POSTFIELDS] = json_encode($corpo);
    }
    $opcoes[CURLOPT_HTTPHEADER] = $headers;

    curl_setopt_array($ch, $opcoes);
    $resposta = curl_exec($ch);
    $status   = (int) curl_getinfo($ch, CURLINFO_HTTP_CODE);
    $erroCurl = curl_error($ch);
    curl_close($ch);

    if ($resposta === false) {
        return ['status' => 0, 'dados' => null, 'erro' => 'Não foi possível conectar à API: ' . $erroCurl];
    }

    return [
        'status' => $status,
        'dados'  => json_decode($resposta, true),
        'erro'   => $status >= 400 ? "A API respondeu com erro $status." : null,
    ];
}

/** Escapa texto para exibir em HTML. */
function h($valor): string
{
    return htmlspecialchars((string) $valor, ENT_QUOTES, 'UTF-8');
}

/**
 * Gera os ícones de estrelas (Font Awesome 6) para uma nota de 0 a 5.
 * Aceita meio ponto: 4.5 = 4 estrelas cheias + 1 meia.
 */
function estrelas(float $nota): string
{
    $nota   = max(0, min(5, round($nota * 2) / 2));
    $cheias = (int) floor($nota);
    $meia   = ($nota - $cheias) >= 0.5;
    $vazias = 5 - $cheias - ($meia ? 1 : 0);

    $html  = str_repeat('<i class="fa-solid fa-star rating-mei"></i>', $cheias);
    $html .= $meia ? '<i class="fa-solid fa-star-half-stroke rating-mei"></i>' : '';
    $html .= str_repeat('<i class="fa-regular fa-star rating-mei"></i>', $vazias);

    return '<span class="estrelas" aria-label="Nota ' . $nota . ' de 5">' . $html . '</span>';
}

/** Guarda uma mensagem para ser exibida na próxima página. */
function flash(string $tipo, string $mensagem): void
{
    $_SESSION['flash'] = ['tipo' => $tipo, 'mensagem' => $mensagem];
}

/** Lê e remove a mensagem guardada. */
function flash_pegar(): ?array
{
    $f = $_SESSION['flash'] ?? null;
    unset($_SESSION['flash']);
    return $f;
}

/** Token CSRF simples para os formulários. */
function csrf_token(): string
{
    if (empty($_SESSION['csrf'])) {
        $_SESSION['csrf'] = bin2hex(random_bytes(16));
    }
    return $_SESSION['csrf'];
}

function csrf_validar(): bool
{
    return isset($_POST['csrf'], $_SESSION['csrf'])
        && hash_equals($_SESSION['csrf'], $_POST['csrf']);
}

/**
 * Endereço de um arquivo da pasta assets.
 * Ex.: asset('css/vitrine.css') => /ProjetoMei/View/assets/css/vitrine.css
 */
function asset(string $caminho): string
{
    return ASSETS_URL . '/' . ltrim($caminho, '/');
}

/**
 * Endereço de uma rota pelo nome definido em routes/rotas.php.
 * Ex.: rota('usuarios') ou rota('categorias', ['cat' => 'design'])
 */
function rota(string $nome, array $params = []): string
{
    if (!isset($GLOBALS['ROTAS'][$nome])) {
        throw new InvalidArgumentException("Rota inexistente: $nome");
    }

    $url = BASE_URL . $GLOBALS['ROTAS'][$nome];

    return $params ? $url . '?' . http_build_query($params) : $url;
}

/** Redireciona para uma rota (pelo nome) e encerra a execução. */
function redirecionar(string $nome, array $params = []): void
{
    header('Location: ' . rota($nome, $params));
    exit;
}