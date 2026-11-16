<?php
/**
 * Rodapé da vitrine (footer + scripts).
 *
 * Variáveis opcionais, definidas pela página antes de incluir:
 *   $rodapeVariante  'padrao' (links rápidos) | 'categorias' (categorias populares + "Precisa de ajuda?")
 *   $scriptsExtras   arquivos JS de assets/js, ex.: ['Contato.js']
 */
$rodapeVariante = $rodapeVariante ?? 'padrao';
$scriptsExtras  = $scriptsExtras ?? [];
$comCategorias  = $rodapeVariante === 'categorias';

if ($comCategorias) {
    $catsRodape = array_slice(require VIEW_PATH . '/pages/dados/Categorias.php', 0, 4);
}
?>
<footer>
    <div class="container">
        <div class="row">
            <div class="col-lg-4">
                <h5>Vitrine MEI</h5>
                <p>Conectando profissionais MEI a clientes desde 2023. Ajudamos microempreendedores a expandir seus negócios.</p>
            </div>

            <div class="col-lg-4">
<?php if ($comCategorias): ?>
                <h5>Categorias Populares</h5>
                <ul class="footer-links">
                    <?php foreach ($catsRodape as $c): ?>
                        <li><a href="<?= h(rota('profissionais', ['categoria' => $c['slug']])) ?>"><?= h($c['nome']) ?></a></li>
                    <?php endforeach; ?>
                </ul>
<?php else: ?>
                <h5>Links Rápidos</h5>
                <ul class="footer-links">
                    <li><a href="<?= rota('como-funciona') ?>">Como Funciona</a></li>
                    <li><a href="<?= rota('cadastro-mei') ?>">Cadastrar MEI</a></li>
                    <li><a href="<?= rota('termos') ?>">Termos de Uso</a></li>
                    <li><a href="<?= rota('privacidade') ?>">Política de Privacidade</a></li>
                </ul>
<?php endif; ?>
            </div>

            <div class="col-lg-4">
                <h5><?= $comCategorias ? 'Precisa de Ajuda?' : 'Contato' ?></h5>
                <p><a href="mailto:suporte@vitrinemei.com.br">suporte@vitrinemei.com.br</a><br>
                   <a href="tel:+5511999999999">(11) 99999-9999</a></p>
                <div class="social-icons">
                    <a href="#" aria-label="Facebook"><i class="fa-brands fa-facebook-f"></i></a>
                    <a href="#" aria-label="Instagram"><i class="fa-brands fa-instagram"></i></a>
                    <a href="#" aria-label="LinkedIn"><i class="fa-brands fa-linkedin-in"></i></a>
                    <a href="#" aria-label="WhatsApp"><i class="fa-brands fa-whatsapp"></i></a>
                </div>
            </div>

            <div class="col-lg-12">
                <div class="sub-footer">
                    <p>Copyright &copy; <?= date('Y') ?> Vitrine MEI - Todos os direitos reservados</p>
                </div>
            </div>
        </div>
    </div>
</footer>

<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
<script src="<?= asset('js/Vitrine.js') ?>"></script>
<?php foreach ($scriptsExtras as $script): ?>
<script src="<?= asset('js/' . $script) ?>"></script>
<?php endforeach; ?>
</body>
</html>