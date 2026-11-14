<?php
/** Página principal da Vitrine MEI. */
$tituloPagina = 'Vitrine MEI - Conectando Profissionais a Clientes';
$paginaAtiva  = 'inicio';
require_once __DIR__ . '/../routes/config.php';
require VIEW_PATH . '/components/layout/topo.php';

$categorias    = require __DIR__ . '/dados/categorias.php';
$profissionais = require __DIR__ . '/dados/profissionais.php';
$slides        = array_chunk($profissionais, 3); // cada slide do carrossel mostra 3 profissionais

$estatisticas = [
    ['5.000+',  'MEIs Cadastrados'],
    ['15.000+', 'Projetos Concluídos'],
    ['4.8',     'Avaliação Média'],
    ['98%',     'Clientes Satisfeitos'],
];

$vantagens = [
    ['MEIs Verificados',      'Todos os profissionais são MEIs regularizados com CNPJ ativo e documentos verificados.'],
    ['Avaliações Reais',      'Sistema de avaliações por projetos concluídos, garantindo transparência e confiança.'],
    ['Orçamentos Gratuitos',  'Solicite orçamentos de múltiplos profissionais e escolha a melhor proposta.'],
];
?>

<!-- ===== Banner + busca ===== -->
<section class="main-banner">
    <div class="container">
        <div class="row">
            <div class="col-lg-12">
                <div class="top-text header-text">
                    <h6>Mais de 5.000+ Profissionais MEI Cadastrados</h6>
                    <h2>Encontre os Melhores Profissionais para Seu Projeto</h2>
                    <p>De marceneiros a programadores, designers a eletricistas - tudo em um só lugar!</p>
                </div>
            </div>

            <div class="col-lg-12">
                <form id="search-form" method="get" role="search" action="<?= rota('profissionais') ?>">
                    <div class="row g-3">
                        <div class="col-lg-4 align-self-center">
                            <select name="categoria" class="form-select" aria-label="Categoria" id="chooseCategory">
                                <option value="" selected>Todas as Categorias</option>
                                <?php foreach ($categorias as $c): ?>
                                    <option value="<?= h($c['slug']) ?>"><?= h($c['opcao']) ?></option>
                                <?php endforeach; ?>
                            </select>
                        </div>
                        <div class="col-lg-4 align-self-center">
                            <input type="text" name="localizacao" class="searchText form-control" placeholder="Cidade, bairro ou estado" autocomplete="on" required>
                        </div>
                        <div class="col-lg-4">
                            <button class="main-button" type="submit"><i class="fa-solid fa-magnifying-glass"></i> Buscar Profissionais</button>
                        </div>
                    </div>
                </form>
            </div>

            <div class="col-lg-10 offset-lg-1">
                <ul class="categories">
                    <?php foreach ($categorias as $c): ?>
                        <li>
                            <a href="<?= h(rota('categorias', ['cat' => $c['slug']])) ?>">
                                <span class="icon"><i class="fa-solid <?= h($c['icone']) ?>"></i></span> <?= h($c['nome']) ?>
                            </a>
                        </li>
                    <?php endforeach; ?>
                </ul>
            </div>
        </div>
    </div>
</section>

<!-- ===== Estatísticas ===== -->
<section class="container estatisticas">
    <div class="row">
        <?php foreach ($estatisticas as [$numero, $rotulo]): ?>
            <div class="col-6 col-md-3">
                <div class="stats-card">
                    <div class="stats-number"><?= h($numero) ?></div>
                    <div class="stats-label"><?= h($rotulo) ?></div>
                </div>
            </div>
        <?php endforeach; ?>
    </div>
</section>

<!-- ===== Categorias em destaque (abas) ===== -->
<section class="popular-categories">
    <div class="container">
        <div class="section-heading">
            <h2>Categorias em Destaque</h2>
            <h6>Encontre profissionais por área de atuação</h6>
        </div>

        <div class="row naccs" data-abas>
            <div class="col-lg-3">
                <div class="menu" role="tablist">
                    <?php foreach ($categorias as $i => $c): ?>
                        <button type="button" role="tab" class="thumb <?= $i === 0 ? 'active' : '' ?>"
                                data-aba="<?= $i ?>" aria-selected="<?= $i === 0 ? 'true' : 'false' ?>">
                            <span class="icon"><i class="fa-solid <?= h($c['icone']) ?>"></i></span> <?= h($c['nome']) ?>
                        </button>
                    <?php endforeach; ?>
                </div>
            </div>

            <div class="col-lg-9 align-self-center">
                <?php foreach ($categorias as $i => $c): ?>
                    <div class="nacc <?= $i === 0 ? 'active' : '' ?>" data-painel="<?= $i ?>" role="tabpanel">
                        <div class="row">
                            <div class="col-lg-5 align-self-center">
                                <div class="left-text">
                                    <h4><?= h($c['titulo']) ?></h4>
                                    <p><?= h($c['texto']) ?></p>
                                    <div class="main-white-button">
                                        <a href="<?= h(rota('categorias', ['cat' => $c['slug']])) ?>"><i class="fa-solid fa-eye"></i> <?= h($c['botao']) ?></a>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-7 align-self-center">
                                <div class="right-image">
                                    <img src="<?= h($c['imagem']) ?>" alt="<?= h($c['alt']) ?>" class="categoria-image" loading="lazy">
                                </div>
                            </div>
                        </div>
                    </div>
                <?php endforeach; ?>
            </div>
        </div>
    </div>
</section>

<!-- ===== Profissionais em destaque (carrossel) ===== -->
<section class="recent-listing">
    <div class="container">
        <div class="section-heading">
            <h2>Profissionais em Destaque</h2>
            <h6>Conheça alguns dos nossos MEIs mais bem avaliados</h6>
        </div>

        <div class="carrossel" data-carrossel>
            <div class="carrossel__janela">
                <div class="carrossel__trilho" data-carrossel-trilho>
                    <?php foreach ($slides as $slide): ?>
                        <div class="carrossel__slide">
                            <div class="row g-4">
                                <?php foreach ($slide as $p): ?>
                                    <div class="col-lg-4 col-md-6">
                                        <?php require VIEW_PATH . '/components/cards/card-profissional.php'; ?>
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
</section>

<!-- ===== Vantagens ===== -->
<section class="container vantagens">
    <div class="section-heading">
        <h2>Vantagens da Vitrine MEI</h2>
        <h6>Por que escolher nossos profissionais</h6>
    </div>
    <div class="row g-4">
        <?php foreach ($vantagens as [$titulo, $texto]): ?>
            <div class="col-md-4">
                <div class="feature-item">
                    <h4><?= h($titulo) ?></h4>
                    <p><?= h($texto) ?></p>
                </div>
            </div>
        <?php endforeach; ?>
    </div>
</section>

<?php require VIEW_PATH . '/components/layout/rodape.php'; ?>