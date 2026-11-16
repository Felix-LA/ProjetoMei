<?php
/**
 * Card de um MEI cadastrado na API.
 * Espera a variável $p, no formato devolvido por vitrine_buscar() (routes/Config.php).
 * Ainda não há foto, nota nem preço no cadastro, por isso o card mostra iniciais e os dados reais.
 */
?>
<article class="listing-item">
    <div class="left-image">
        <div class="mei-avatar" aria-hidden="true"><?= h($p['iniciais']) ?></div>
        <span class="mei-badge"><?= $p['premium'] ? 'Premium' : 'MEI Cadastrado' ?></span>
    </div>

    <div class="right-content">
        <h4><?= h($p['nome']) ?></h4>
        <h6><?= h($p['ramo']) ?></h6>

        <div class="mb-2">
            <?php foreach (array_slice($p['tags'], 0, 4) as $tag): ?>
                <span class="service-tag"><?= h($tag) ?></span>
            <?php endforeach; ?>
        </div>

        <p class="mei-descricao"><?= h(mb_strimwidth($p['descricao'], 0, 110, '…')) ?></p>

        <ul class="info">
            <?php if ($p['local'] !== ''): ?>
                <li><i class="fa-solid fa-location-dot"></i> <?= h($p['local']) ?></li>
            <?php endif; ?>
            <?php if ($p['experiencia'] !== ''): ?>
                <li><i class="fa-solid fa-briefcase"></i> <?= h($p['experiencia']) ?></li>
            <?php endif; ?>
        </ul>

        <div class="main-white-button">
            <a href="<?= h(rota('contato')) ?>"><i class="fa-brands fa-whatsapp"></i> Solicitar Orçamento</a>
        </div>
    </div>
</article>