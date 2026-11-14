<?php
/**
 * Card de um profissional da vitrine.
 * Espera a variável $p (um item de pages/dados/profissionais.php).
 */
?>
<article class="listing-item">
    <div class="left-image">
        <a href="#"><img src="<?= h($p['imagem']) ?>" alt="<?= h($p['titulo']) ?>" class="profissional-image" loading="lazy"></a>
        <span class="mei-badge">MEI Regularizado</span>
    </div>

    <div class="right-content">
        <a href="#">
            <h4><?= h($p['nome']) ?> <i class="fa-solid fa-circle-check verificado" title="Verificado"></i></h4>
        </a>
        <h6><?= h($p['titulo']) ?></h6>

        <div class="mb-2">
            <?php foreach ($p['tags'] as $tag): ?>
                <span class="service-tag"><?= h($tag) ?></span>
            <?php endforeach; ?>
        </div>

        <ul class="rate">
            <li><?= estrelas((float) $p['nota']) ?></li>
            <li>(<?= (int) $p['avaliacoes'] ?>) Avaliações</li>
        </ul>

        <span class="price"><i class="fa-solid fa-money-bill-wave"></i> <?= h($p['preco']) ?></span>
        <span class="details"><?= h($p['detalhe'][0]) ?>: <em><?= h($p['detalhe'][1]) ?></em></span>

        <ul class="info">
            <?php foreach ($p['info'] as [$icone, $texto]): ?>
                <li><i class="fa-solid <?= h($icone) ?>"></i> <?= h($texto) ?></li>
            <?php endforeach; ?>
        </ul>

        <div class="main-white-button">
            <a href="<?= Rotas('contato') ?>"><i class="fa-brands fa-whatsapp"></i> Solicitar Orçamento</a>
        </div>
    </div>
</article>