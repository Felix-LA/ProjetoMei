/**
 * app.js — comportamentos globais: modais e alertas.
 * Uso no HTML:
 *   data-modal-abrir="id-do-modal"  -> abre o modal
 *   data-modal-fechar               -> fecha o modal onde está
 */
(function () {
    'use strict';

    const Modal = {
        abrir(id) {
            const modal = document.getElementById(id);
            if (!modal) return;
            modal.classList.add('modal--aberto');
            modal.setAttribute('aria-hidden', 'false');
            document.body.classList.add('sem-scroll');
            const primeiro = modal.querySelector('input:not([type=hidden]), button');
            if (primeiro) setTimeout(() => primeiro.focus(), 50);
        },
        fechar(modal) {
            if (!modal) return;
            modal.classList.remove('modal--aberto');
            modal.setAttribute('aria-hidden', 'true');
            if (!document.querySelector('.modal--aberto')) {
                document.body.classList.remove('sem-scroll');
            }
        },
    };
    window.Modal = Modal;

    document.addEventListener('click', function (e) {
        const abrir = e.target.closest('[data-modal-abrir]');
        if (abrir) {
            Modal.abrir(abrir.dataset.modalAbrir);
            return;
        }

        if (e.target.closest('[data-modal-fechar]') || e.target.classList.contains('modal')) {
            Modal.fechar(e.target.closest('.modal'));
            return;
        }

        const fecharAlerta = e.target.closest('[data-alerta-fechar]');
        if (fecharAlerta) fecharAlerta.closest('[data-alerta]').remove();
    });

    document.addEventListener('keydown', function (e) {
        if (e.key === 'Escape') {
            Modal.fechar(document.querySelector('.modal--aberto'));
        }
    });

    // Alertas de sucesso somem sozinhos
    document.querySelectorAll('.alerta--sucesso[data-alerta]').forEach(function (a) {
        setTimeout(() => a.remove(), 4000);
    });
})();