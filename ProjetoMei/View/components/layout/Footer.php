</main>

<footer class="rodape">
    <div class="container rodape__grade">
        <div>
            <h3 class="rodape__titulo">Projeto <strong>MEI</strong></h3>
            <p>Vitrine online que aproxima microempreendedores individuais de quem precisa de um serviço.</p>
        </div>

        <div>
            <h3 class="rodape__titulo">Contato</h3>
            <ul class="rodape__lista">
                <li>E-mail: <a href="mailto:contato@projetomei.exemplo">contato@projetomei.exemplo</a></li>
                <li>Telefone: <a href="tel:+5567999990000">(67) 99999-0000</a></li>
                <li>Atendimento: seg. a sex., 8h às 18h</li>
            </ul>
        </div>

        <div>
            <h3 class="rodape__titulo">O site</h3>
            <ul class="rodape__lista">
                <li><a href="<?= Rotas('inicio') ?>">Início</a></li>
                <li><a href="<?= Rotas('inicio') ?>#vitrine">Vitrine</a></li>
                <li><a href="<?= Rotas('login') ?>">Login</a></li>
            </ul>
        </div>
    </div>
    <div class="container rodape__base">&copy; <?= date('Y') ?> Projeto MEI. Todos os direitos reservados.</div>
</footer>

<script src="<?= assets('js/app.js') ?>"></script>
<?php foreach (($scriptsExtras ?? []) as $script): ?>
<script src="<?= assets('js/' . $script) ?>"></script>
<?php endforeach; ?>
</body>
</html>