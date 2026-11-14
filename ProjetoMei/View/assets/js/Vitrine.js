/**
 * vitrine.js — comportamentos da página principal (sem jQuery):
 *  1. menu do celular
 *  2. abas das categorias em destaque
 *  3. carrossel dos profissionais
 */
(function () {
    'use strict';

    /* ---------- 1. Menu do celular ---------- */
    const botaoMenu = document.querySelector('[data-menu-botao]');
    const menu = document.querySelector('[data-menu]');

    if (botaoMenu && menu) {
        botaoMenu.addEventListener('click', function () {
            const aberto = menu.classList.toggle('nav--aberto');
            botaoMenu.setAttribute('aria-expanded', aberto ? 'true' : 'false');
        });
    }

    /* ---------- 2. Abas das categorias ---------- */
    const abas = document.querySelector('[data-abas]');

    if (abas) {
        const botoes = abas.querySelectorAll('[data-aba]');
        const paineis = abas.querySelectorAll('[data-painel]');

        botoes.forEach(function (botao) {
            botao.addEventListener('click', function () {
                const alvo = botao.dataset.aba;
                botoes.forEach(function (b) {
                    const ativo = b === botao;
                    b.classList.toggle('active', ativo);
                    b.setAttribute('aria-selected', ativo ? 'true' : 'false');
                });
                paineis.forEach(function (p) {
                    p.classList.toggle('active', p.dataset.painel === alvo);
                });
            });
        });
    }

    /* ---------- 3. Carrossel ---------- */
    const raiz = document.querySelector('[data-carrossel]');
    if (!raiz) return;

    const trilho = raiz.querySelector('[data-carrossel-trilho]');
    const slides = Array.from(trilho.children);
    const pontos = raiz.querySelector('[data-carrossel-pontos]');
    const anterior = raiz.querySelector('[data-carrossel-anterior]');
    const proximo = raiz.querySelector('[data-carrossel-proximo]');
    const INTERVALO = 6000;

    let indice = 0;
    let timer = null;

    function ir(novo) {
        indice = (novo + slides.length) % slides.length;
        trilho.style.transform = 'translateX(' + (-indice * 100) + '%)';
        if (pontos) {
            Array.from(pontos.children).forEach(function (p, i) {
                p.classList.toggle('carrossel__ponto--ativo', i === indice);
            });
        }
    }

    function parar() { if (timer) { clearInterval(timer); timer = null; } }
    function iniciar() {
        parar();
        if (slides.length > 1) timer = setInterval(function () { ir(indice + 1); }, INTERVALO);
    }

    if (pontos) {
        slides.forEach(function (_, i) {
            const b = document.createElement('button');
            b.type = 'button';
            b.className = 'carrossel__ponto';
            b.setAttribute('aria-label', 'Ir para o grupo ' + (i + 1));
            b.addEventListener('click', function () { ir(i); iniciar(); });
            pontos.appendChild(b);
        });
    }

    if (anterior) anterior.addEventListener('click', function () { ir(indice - 1); iniciar(); });
    if (proximo) proximo.addEventListener('click', function () { ir(indice + 1); iniciar(); });

    raiz.addEventListener('mouseenter', parar);
    raiz.addEventListener('mouseleave', iniciar);
    raiz.addEventListener('focusin', parar);
    raiz.addEventListener('focusout', iniciar);

    // Deslizar com o dedo
    let inicioX = null;
    raiz.addEventListener('touchstart', function (e) { inicioX = e.touches[0].clientX; parar(); }, { passive: true });
    raiz.addEventListener('touchend', function (e) {
        if (inicioX !== null) {
            const dx = e.changedTouches[0].clientX - inicioX;
            if (Math.abs(dx) > 40) ir(indice + (dx < 0 ? 1 : -1));
        }
        inicioX = null;
        iniciar();
    });

    ir(0);
    iniciar();
})();