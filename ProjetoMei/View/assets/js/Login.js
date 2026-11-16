/**
 * Login.js — validação do formulário de login e botão de mostrar/ocultar senha.
 * (A validação real continua no servidor, em Autenticar.php.)
 */
(function () {
    'use strict';

    const form = document.getElementById('form-login');
    if (!form) return;

    const email = document.getElementById('login-email');
    const senha = document.getElementById('login-senha');
    const verSenha = form.querySelector('[data-ver-senha]');

    function marcar(campo, valido) {
        const aviso = form.querySelector('[data-erro="' + campo.name + '"]');
        aviso.hidden = valido;
        campo.classList.toggle('invalido', !valido);
        return valido;
    }

    form.addEventListener('submit', function (e) {
        const emailOk = marcar(email, /^\S+@\S+\.\S+$/.test(email.value.trim()));
        const senhaOk = marcar(senha, senha.value.length > 0);
        if (!emailOk || !senhaOk) e.preventDefault();
    });

    verSenha.addEventListener('click', function () {
        const mostrando = senha.type === 'text';
        senha.type = mostrando ? 'password' : 'text';
        verSenha.textContent = mostrando ? 'Mostrar' : 'Ocultar';
        verSenha.setAttribute('aria-label', mostrando ? 'Mostrar senha' : 'Ocultar senha');
    });
})();