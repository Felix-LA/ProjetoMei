<?php
/**
 * Categorias de profissionais MEI.
 * Aceita ?cat=slug para abrir já na categoria escolhida (links da página inicial).
 */
$tituloPagina    = 'Categorias de Profissionais MEI - Vitrine MEI';
$descricaoPagina = 'Explore todas as categorias de profissionais MEI disponíveis na Vitrine MEI. Encontre marceneiros, eletricistas, programadores, designers e muito mais.';
$paginaAtiva     = 'categorias';
$estilosExtras   = ['Categoria.css'];
$rodapeVariante  = 'categorias';

require_once __DIR__ . '/../routes/Config.php';
require VIEW_PATH . '/components/layout/Topo.php';

$categorias = require __DIR__ . '/dados/Categorias.php';          // ícones por categoria
$detalhes   = require __DIR__ . '/dados/CategoriasDetalhes.php';  // conteúdo de cada painel
$icones     = array_column($categorias, 'icone', 'slug');

// Categoria aberta: a do parâmetro ?cat=, se for válida; senão a primeira.
$pedida = isset($_GET['cat']) && is_string($_GET['cat']) ? $_GET['cat'] : '';
$ativa  = array_search($pedida, array_column($detalhes, 'slug'), true);
$ativa  = $ativa === false ? 0 : $ativa;
?>

<!-- ===== Cabeçalho da página ===== -->
<section class="page-heading">
    <div class="container">
        <div class="row align-items-center g-4">
            <div class="col-lg-8">
                <div class="top-text header-text">
                    <h6>Explore Nossas Categorias de Profissionais</h6>
                    <h2>Encontre o Profissional MEI Ideal para Seu Projeto</h2>
                    <p>Temos uma ampla gama de categorias de serviços oferecidos por profissionais MEI regularizados</p>
                </div>
            </div>
            <div class="col-lg-4">
                <div class="mei-stats text-center">
                    <div class="stats-number"><?= count(vitrine_buscar()['itens']) ?></div>
                    <div class="stats-label">Profissionais MEI</div>
                </div>
            </div>
        </div>
    </div>
</section>

<!-- ===== Categorias (abas) ===== -->
<section class="category-post categoria">
    <div class="container">
        <div class="naccs" data-abas>

            <div class="menu menu--horizontal" role="tablist">
                <?php foreach ($detalhes as $i => $d): ?>
                    <button type="button" role="tab" class="thumb <?= $i === $ativa ? 'active' : '' ?>"
                            data-aba="<?= $i ?>" aria-selected="<?= $i === $ativa ? 'true' : 'false' ?>">
                        <span class="icon"><i class="fa-solid <?= h($icones[$d['slug']] ?? 'fa-briefcase') ?>"></i></span>
                        <?= h($d['nome']) ?>
                    </button>
                <?php endforeach; ?>
            </div>

            <?php foreach ($detalhes as $i => $d): ?>
                <div class="nacc <?= $i === $ativa ? 'active' : '' ?>" data-painel="<?= $i ?>" role="tabpanel">

                    <!-- Topo do painel -->
                    <div class="cat-topo">
                        <div class="cat-topo__titulo">
                            <span class="cat-icone"><i class="fa-solid <?= h($icones[$d['slug']] ?? 'fa-briefcase') ?>"></i></span>
                            <div>
                                <h4><?= h($d['titulo_topo']) ?></h4>
                                <span class="category-badge"><?= h($d['badge']) ?></span>
                            </div>
                        </div>
                        <div class="main-white-button">
                            <a href="<?= h(rota('profissionais', ['categoria' => $d['slug']])) ?>"><i class="fa-solid fa-eye"></i> Ver Profissionais</a>
                        </div>
                    </div>

                    <!-- Descrição + estatísticas -->
                    <div class="row g-4">
                        <div class="col-lg-9">
                            <h4><?= h($d['titulo']) ?></h4>
                            <p><?= h($d['texto']) ?></p>

                            <div class="category-highlight">
                                <h5><?= h($d['destaque']) ?></h5>
                                <ul class="service-list">
                                    <?php foreach ($d['servicos'] as $servico): ?>
                                        <li><?= h($servico) ?></li>
                                    <?php endforeach; ?>
                                </ul>
                            </div>

                            <?php if ($d['verificacao']): ?>
                                <div class="verification-info">
                                    <h6><i class="fa-solid <?= h($d['verificacao']['icone']) ?>"></i> <?= h($d['verificacao']['titulo']) ?></h6>
                                    <p><?= h($d['verificacao']['texto']) ?></p>
                                </div>
                            <?php endif; ?>

                            <?php if ($d['nota']): ?>
                                <p><strong><?= h($d['nota']['rotulo']) ?>:</strong> <?= h($d['nota']['texto']) ?></p>
                            <?php endif; ?>
                        </div>

                        <div class="col-lg-3">
                            <aside class="cat-stats">
                                <h5><i class="fa-solid fa-chart-column"></i> Estatísticas</h5>
                                <dl>
                                    <?php foreach ($d['estatisticas'] as [$rotulo, $valor]): ?>
                                        <div>
                                            <dt><?= h($rotulo) ?></dt>
                                            <dd><?= h($valor) ?></dd>
                                        </div>
                                    <?php endforeach; ?>
                                </dl>
                            </aside>
                        </div>
                    </div>

                    <!-- Como contratar -->
                    <div class="general-info">
                        <h4><?= h($d['contratar']['titulo']) ?></h4>
                        <p><?= h($d['contratar']['intro']) ?></p>
                        <ol class="passos">
                            <?php foreach ($d['contratar']['passos'] as $passo): ?>
                                <li><?= h($passo) ?></li>
                            <?php endforeach; ?>
                        </ol>
                        <?php if ($d['contratar']['dica']): ?>
                            <p class="dica"><strong><?= h($d['contratar']['dica']['rotulo']) ?>:</strong> <?= h($d['contratar']['dica']['texto']) ?></p>
                        <?php endif; ?>
                    </div>

                </div>
            <?php endforeach; ?>

        </div>
    </div>
</section>

<!-- ===== Chamada final ===== -->
<section class="container busca-avancada">
    <h3>Não Encontrou o Que Procurava?</h3>
    <p>Temos muitas outras subcategorias disponíveis. Use nossa busca avançada ou entre em contato conosco.</p>
    <div class="main-white-button">
        <a href="<?= rota('profissionais') ?>"><i class="fa-solid fa-magnifying-glass"></i> Busca Avançada</a>
    </div>
</section>

<?php require VIEW_PATH . '/components/layout/Rodape.php'; ?>