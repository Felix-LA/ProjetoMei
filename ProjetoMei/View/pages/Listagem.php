<?php
/**
 * Listagem de profissionais, em abas por categoria e carrossel por "página".
 * Os dados vêm da API (GET /api/UsuarioMei/vitrine); só aparecem abas de categorias
 * que têm pelo menos um MEI cadastrado.
 */
$tituloPagina    = 'Profissionais - Vitrine MEI';
$descricaoPagina = 'Veja os profissionais MEI da Vitrine MEI, organizados por categoria.';
$paginaAtiva     = 'profissionais';
$estilosExtras   = ['Listagem.css'];

require_once __DIR__ . '/../routes/Config.php';
require VIEW_PATH . '/components/layout/Topo.php';

$vitrine = vitrine_buscar();

// Filtros vindos da busca da home e da página de categorias:
//   ?categoria=eletricista  -> abre a aba dessa categoria (se houver MEIs nela)
//   ?localizacao=campo grande -> mostra só MEIs cujo "cidade - UF" contém o texto
$catPedida   = isset($_GET['categoria']) && is_string($_GET['categoria']) ? trim($_GET['categoria']) : '';
$localPedido = isset($_GET['localizacao']) && is_string($_GET['localizacao']) ? trim($_GET['localizacao']) : '';

$itens = $vitrine['itens'];
if ($localPedido !== '') {
    $agulha = mb_strtolower($localPedido);
    $itens  = array_values(array_filter($itens, fn ($m) => str_contains(mb_strtolower($m['local']), $agulha)));
}

// Abas: "Todos" + uma por categoria em uso (na ordem em que aparecem no cadastro).
$grupos = [];
if ($itens) {
    $grupos[] = ['chave' => '', 'nome' => 'Todos', 'icone' => 'fa-layer-group', 'itens' => $itens];

    foreach ((require __DIR__ . '/dados/CadastroMei.php')['categorias'] as [$chave, $rotulo]) {
        $doGrupo = array_values(array_filter($itens, fn ($m) => in_array($chave, $m['chaves'], true)));
        if ($doGrupo) {
            $grupos[] = ['chave' => $chave, 'nome' => preg_replace('/^[^\p{L}]+/u', '', $rotulo), 'icone' => 'fa-tag', 'itens' => $doGrupo];
        }
    }
}

// Aba aberta: a da categoria pedida, se existir; senão "Todos".
$ativa = 0;
foreach ($grupos as $i => $g) {
    if ($catPedida !== '' && $g['chave'] === $catPedida) { $ativa = $i; break; }
}
?>

<!-- ===== Cabeçalho da página ===== -->
<section class="page-heading">
    <div class="container">
        <div class="row">
            <div class="col-lg-8">
                <div class="top-text header-text">
                    <h6>Encontre seu profissional</h6>
                    <h2>MEIs cadastrados por categoria</h2>
                </div>
            </div>
        </div>
    </div>
</section>

<!-- ===== Abas + carrosséis ===== -->
<section class="listing-page listagem">
    <div class="container">
        <?php if (!$vitrine['ok']): ?>
            <div class="vitrine-vazia">
                <i class="fa-solid fa-plug-circle-xmark"></i>
                <p class="mb-0">Não foi possível carregar os profissionais agora. Tente novamente em instantes.</p>
            </div>
        <?php elseif (!$grupos): ?>
            <div class="vitrine-vazia">
                <i class="fa-solid fa-user-plus"></i>
                <?php if ($localPedido !== ''): ?>
                    <p class="mb-0">Nenhum MEI encontrado em “<?= h($localPedido) ?>”.</p>
                    <div class="main-white-button"><a href="<?= h(rota('profissionais')) ?>">Ver todos os profissionais</a></div>
                <?php else: ?>
                    <p class="mb-0">Ainda não há MEIs cadastrados.</p>
                    <div class="main-white-button"><a href="<?= h(rota('cadastro-mei')) ?>">Cadastrar meu MEI</a></div>
                <?php endif; ?>
            </div>
        <?php else: ?>
        <?php if ($localPedido !== ''): ?>
            <p class="mb-3">Mostrando MEIs em “<strong><?= h($localPedido) ?></strong>”.
                <a href="<?= h(rota('profissionais')) ?>">Limpar filtro</a></p>
        <?php endif; ?>
        <div class="row g-4 naccs" data-abas>

            <div class="col-lg-3">
                <div class="menu" role="tablist">
                    <?php foreach ($grupos as $i => $g): ?>
                        <button type="button" role="tab" class="thumb <?= $i === $ativa ? 'active' : '' ?>"
                                data-aba="<?= $i ?>" aria-selected="<?= $i === $ativa ? 'true' : 'false' ?>">
                            <span class="icon"><i class="fa-solid <?= h($g['icone']) ?>"></i></span>
                            <?= h($g['nome']) ?> <small>(<?= count($g['itens']) ?>)</small>
                        </button>
                    <?php endforeach; ?>
                </div>
            </div>

            <div class="col-lg-9">
                <?php foreach ($grupos as $i => $g): ?>
                    <?php $slides = array_chunk($g['itens'], 4); // 4 cards por página do carrossel ?>
                    <div class="nacc <?= $i === $ativa ? 'active' : '' ?>" data-painel="<?= $i ?>" role="tabpanel">

                        <div class="carrossel" data-carrossel>
                            <div class="carrossel__janela">
                                <div class="carrossel__trilho" data-carrossel-trilho>
                                    <?php foreach ($slides as $slide): ?>
                                        <div class="carrossel__slide">
                                            <div class="row g-4">
                                                <?php foreach ($slide as $p): ?>
                                                    <div class="col-md-6">
                                                        <?php require __DIR__ . '/CardProfissional.php'; ?>
                                                    </div>
                                                <?php endforeach; ?>
                                            </div>
                                        </div>
                                    <?php endforeach; ?>
                                </div>
                            </div>

                            <?php if (count($slides) > 1): ?>
                                <div class="carrossel__controles">
                                    <button type="button" class="carrossel__seta" data-carrossel-anterior aria-label="Anterior">&#8249;</button>
                                    <div class="carrossel__pontos" data-carrossel-pontos></div>
                                    <button type="button" class="carrossel__seta" data-carrossel-proximo aria-label="Próximo">&#8250;</button>
                                </div>
                            <?php endif; ?>
                        </div>

                    </div>
                <?php endforeach; ?>
            </div>

        </div>
        <?php endif; ?>
    </div>
</section>

<?php require VIEW_PATH . '/components/layout/Rodape.php'; ?>