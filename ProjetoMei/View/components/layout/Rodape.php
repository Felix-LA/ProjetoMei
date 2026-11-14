<footer>
    <div class="container">
        <div class="row">
            <div class="col-lg-4">
                <h5>Vitrine MEI</h5>
                <p>Conectando profissionais MEI a clientes desde 2023. Ajudamos microempreendedores a expandir seus negócios.</p>
            </div>
            <div class="col-lg-4">
                <h5>Links Rápidos</h5>
                <ul class="footer-links">
                    <li><a href="<?= Rotas('como-funciona') ?>">Como Funciona</a></li>
                    <li><a href="<?= Rotas('cadastro-mei') ?>">Cadastrar MEI</a></li>
                    <li><a href="<?= Rotas('termos') ?>">Termos de Uso</a></li>
                    <li><a href="<?= Rotas('privacidade') ?>">Política de Privacidade</a></li>
                </ul>
            </div>
            <div class="col-lg-4">
                <h5>Contato</h5>
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
<script src="<?= assets('js/vitrine.js') ?>"></script>
</body>
</html>