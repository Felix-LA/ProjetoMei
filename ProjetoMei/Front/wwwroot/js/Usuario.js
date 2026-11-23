/**
 * Usuario.js — comportamentos da tela de usuários:
 * preencher os modais de novo/editar/excluir e validar o formulário.
 */
(function () {
    'use strict';

    const form = document.getElementById('form-usuario');
    const campoId = document.getElementById('usuario-id');
    const campoNome = document.getElementById('usuario-nome');
    const titulo = document.querySelector('#modal-usuario [data-titulo]');
    const erro = form ? form.querySelector('[data-erro]') : null;

    // Prepara o conteúdo dos modais antes de abrirem (o App.js abre em seguida)
    document.addEventListener('click', function (e) {
        const gatilho = e.target.closest('[data-acao]');
        if (!gatilho) return;

        const { acao, id, nome } = gatilho.dataset;

        if (acao === 'novo' || acao === 'editar') {
            campoId.value = acao === 'editar' ? id : '';
            campoNome.value = acao === 'editar' ? nome : '';
            titulo.textContent = acao === 'editar' ? 'Editar usuário' : 'Novo usuário';
            erro.hidden = true;
            campoNome.classList.remove('invalido');
        }

        if (acao === 'excluir') {
            document.getElementById('excluir-id').value = id;
            document.querySelector('[data-nome-excluir]').textContent = nome;
        }
    }, true); // captura: roda antes do handler de abrir do App.js

    // Validação no cliente (a validação real continua no servidor)
    if (form) {
        form.addEventListener('submit', function (e) {
            const valor = campoNome.value.trim();
            const valido = valor.length > 0 && valor.length <= 100;
            erro.hidden = valido;
            campoNome.classList.toggle('invalido', !valido);
            if (!valido) e.preventDefault();
        });
    }

    // Abre o modal de cadastro direto se vier de ?novo=1 (atalho da página inicial)
    if (new URLSearchParams(location.search).get('novo') === '1') {
        const botao = document.querySelector('[data-acao="novo"]');
        if (botao) botao.click();
    }
})();