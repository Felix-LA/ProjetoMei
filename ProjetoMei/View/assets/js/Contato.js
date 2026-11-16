/**
 * Contato.js — comportamentos da página Contato.php:
 *  1. perguntas frequentes (acordeão: só uma aberta por vez)
 *  2. formulário de mensagem (validação e aviso na própria página)
 *
 * ATENÇÃO: ainda não existe endpoint para receber a mensagem; o formulário só valida
 * e mostra o aviso de sucesso (igual ao comportamento do HTML original).
 */
(function () {
    'use strict';

    /* ---------- 1. Perguntas frequentes ---------- */
    const perguntas = Array.from(document.querySelectorAll('.faq-question'));

    function definirAberta(pergunta, aberta) {
        pergunta.classList.toggle('active', aberta);
        pergunta.setAttribute('aria-expanded', aberta ? 'true' : 'false');
        pergunta.nextElementSibling.style.display = aberta ? 'block' : 'none';
    }

    perguntas.forEach(function (pergunta) {
        pergunta.addEventListener('click', function () {
            const estavaAberta = pergunta.classList.contains('active');
            perguntas.forEach(function (outra) { definirAberta(outra, false); });
            if (!estavaAberta) definirAberta(pergunta, true);
        });
    });

    // A primeira pergunta já começa aberta
    if (perguntas.length) definirAberta(perguntas[0], true);

    /* ---------- 2. Formulário ---------- */
    const form = document.getElementById('contact-form');
    const aviso = document.getElementById('contato-aviso');
    if (!form) return;

    function mostrarAviso(tipo, mensagem) {
        aviso.className = 'alert alert-' + tipo;
        aviso.textContent = mensagem;
        aviso.scrollIntoView({ behavior: 'smooth', block: 'center' });
    }

    form.addEventListener('submit', function (e) {
        e.preventDefault();

        const obrigatorios = ['name', 'email', 'subject', 'tipo-contato', 'message'];
        let valido = true;

        obrigatorios.forEach(function (id) {
            const campo = document.getElementById(id);
            const vazio = !campo.value.trim();
            campo.classList.toggle('is-invalid', vazio);
            if (vazio) valido = false;
        });

        const email = document.getElementById('email');
        if (email.value.trim() && !/^\S+@\S+\.\S+$/.test(email.value.trim())) {
            email.classList.add('is-invalid');
            valido = false;
        }

        if (!valido) {
            mostrarAviso('danger', 'Por favor, preencha todos os campos obrigatórios corretamente.');
            return;
        }

        mostrarAviso('success', 'Obrigado! Sua mensagem foi enviada com sucesso. Entraremos em contato em breve.');
        form.reset();
    });

    // Tira o destaque de erro assim que o campo é corrigido
    form.addEventListener('input', function (e) {
        if (e.target.classList.contains('is-invalid') && e.target.value.trim()) {
            e.target.classList.remove('is-invalid');
        }
    });
})();