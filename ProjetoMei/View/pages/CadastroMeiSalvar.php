<?php
/**
 * Recebe o cadastro de MEI (enviado por fetch em CadastroMei.js) e grava na API.
 * Responde sempre em JSON: { ok: bool, mensagem: string }.
 * Os documentos (RG, comprovante, foto) ainda NÃO são enviados: a API só recebe os dados.
 */
require_once __DIR__ . '/../routes/Config.php';

header('Content-Type: application/json; charset=utf-8');

function responder(int $status, bool $ok, string $mensagem): never
{
    http_response_code($status);
    echo json_encode(['ok' => $ok, 'mensagem' => $mensagem], JSON_UNESCAPED_UNICODE);
    exit;
}

if ($_SERVER['REQUEST_METHOD'] !== 'POST' || !csrf_validar()) {
    responder(403, false, 'Sessão expirada. Recarregue a página e tente de novo.');
}

$campo = fn (string $nome): string => trim((string) ($_POST[$nome] ?? ''));

$corpo = [
    'nome'          => $campo('nome'),
    'cpf'           => $campo('cpf'),
    'cnpj'          => $campo('cnpj'),
    'email'         => $campo('email'),
    'telefone'      => $campo('telefone'),
    'cidade'        => $campo('cidade'),
    'estado'        => $campo('estado'),
    'ramoAtividade' => $campo('ramo-atividade'),
    'descricao'     => $campo('descricao'),
    'experiencia'   => $campo('experiencia'),
    'portfolio'     => $campo('portfolio'),
    'categorias'    => $campo('categorias'),
    'plano'         => $campo('plano') ?: 'gratuito',
];

if (empty($_POST['termos'])) {
    responder(422, false, 'Você deve aceitar os Termos de Uso para continuar.');
}

$resp = api('POST', '', $corpo);

if ($resp['status'] === 201 || $resp['status'] === 200) {
    responder(201, true, 'Cadastro realizado com sucesso.');
}

if ($resp['status'] === 0) {
    responder(503, false, 'Não foi possível conectar à API. Tente novamente em instantes.');
}

// 400 (validação) e 409 (CPF/e-mail já cadastrado) trazem a mensagem da API.
$http = in_array($resp['status'], [400, 409], true) ? $resp['status'] : 502;
responder($http, false, api_mensagem($resp));