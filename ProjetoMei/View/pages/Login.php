<?php
/** Tela de login: só é aberta ao clicar em "Login" no header. */
$tituloPagina  = 'Login';
$paginaAtiva   = 'login';
$scriptsExtras = ['login.js'];
require_once __DIR__ . '/../routes/config.php';
require VIEW_PATH . '/components/layout/header.php';
?>

<section class="login">
    <div class="login__caixa">
        <h1>Entrar</h1>
        <p class="login__sub">Acesse sua conta para gerenciar seu perfil.</p>

        <form method="post" action="<?= rota('autenticar') ?>" id="form-login" novalidate>
            <input type="hidden" name="csrf" value="<?= h(csrf_token()) ?>">

            <div class="campo">
                <label for="login-email">E-mail</label>
                <input type="email" id="login-email" name="email" autocomplete="email" required>
                <small class="campo__erro" data-erro="email" hidden>Informe um e-mail válido.</small>
            </div>

            <div class="campo">
                <label for="login-senha">Senha</label>
                <div class="campo__senha">
                    <input type="password" id="login-senha" name="senha" autocomplete="current-password" required>
                    <button type="button" class="campo__ver" data-ver-senha aria-label="Mostrar senha">Mostrar</button>
                </div>
                <small class="campo__erro" data-erro="senha" hidden>Informe a senha.</small>
            </div>

            <button type="submit" class="btn login__enviar">Entrar</button>
        </form>

        <a class="login__voltar" href="<?= rota('inicio') ?>">&larr; Voltar ao início</a>
    </div>
</section>

<?php require VIEW_PATH . '/components/layout/footer.php'; ?>