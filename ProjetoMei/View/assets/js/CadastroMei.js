/**
 * CadastroMei.js — comportamentos da página CadastroMei.php:
 *  navegação entre os 4 passos, validação, categorias, envio de documentos,
 *  escolha de plano e máscaras de CPF / CNPJ / telefone.
 *
 * Ao finalizar, os dados de texto são enviados por fetch a CadastroMeiSalvar.php, que
 * grava na API. O painel de sucesso só aparece se a API aceitar. Os documentos (RG,
 * comprovante, foto) ainda não são enviados.
 */
(function () {
    'use strict';

    const raiz = document.querySelector('[data-cadastro]');
    const form = document.getElementById('cadastro-mei-form');
    if (!raiz || !form) return;

    const TOTAL_PASSOS = 4;
    const TAMANHO_MAX = 5 * 1024 * 1024; // 5 MB
    const aviso = document.getElementById('cadastro-aviso');
    let passoAtual = 1;

    /* ---------- Utilidades ---------- */
    const $ = function (id) { return document.getElementById(id); };
    const soDigitos = function (texto) { return texto.replace(/\D/g, ''); };

    function mostrarAviso(mensagem) {
        aviso.textContent = mensagem;
        aviso.classList.remove('d-none');
    }

    function limparAviso() {
        aviso.textContent = '';
        aviso.classList.add('d-none');
    }

    function marcar(elemento, invalido) {
        elemento.classList.toggle('is-invalid', invalido);
        return !invalido;
    }

    /* ---------- Navegação entre passos ---------- */
    function atualizarIndicador(passo) {
        for (let i = 1; i <= TOTAL_PASSOS; i++) {
            $('step' + i + '-indicator').classList.toggle('active', i <= passo);
        }
    }

    function irPara(passo) {
        $('step' + passoAtual).classList.remove('active');
        $('step' + passo).classList.add('active');
        passoAtual = passo;
        atualizarIndicador(passo);
        limparAviso();
        $('indicador-passos').scrollIntoView({ behavior: 'smooth', block: 'start' });
    }

    /* ---------- Validação de cada passo ---------- */
    function validarPasso(passo) {
        let valido = true;
        let mensagem = 'Por favor, preencha todos os campos obrigatórios corretamente.';

        if (passo === 1) {
            ['nome', 'cidade', 'estado'].forEach(function (id) {
                valido = marcar($(id), !$(id).value.trim()) && valido;
            });
            valido = marcar($('cpf'), soDigitos($('cpf').value).length !== 11) && valido;
            valido = marcar($('email'), !/^\S+@\S+\.\S+$/.test($('email').value.trim())) && valido;
            valido = marcar($('telefone'), soDigitos($('telefone').value).length < 10) && valido;

            if (!$('termos').checked) {
                valido = false;
                mensagem = 'Você deve aceitar os Termos de Uso para continuar.';
            }
        }

        if (passo === 2) {
            ['ramo-atividade', 'descricao'].forEach(function (id) {
                valido = marcar($(id), !$(id).value.trim()) && valido;
            });
            valido = marcar($('cnpj'), soDigitos($('cnpj').value).length !== 14) && valido;

            if (!document.querySelector('.categoria-tag.selected')) {
                valido = false;
                mensagem = 'Selecione pelo menos uma categoria de serviços.';
            }
        }

        if (passo === 3) {
            form.querySelectorAll('input[type=file][data-obrigatorio]').forEach(function (input) {
                const caixa = form.querySelector('[data-arquivo-abrir="' + input.dataset.arquivo + '"]');
                valido = marcar(caixa, input.files.length === 0) && valido;
            });
            if (!valido) mensagem = 'Envie os documentos obrigatórios para continuar.';
        }

        if (!valido) mostrarAviso(mensagem);
        return valido;
    }

    /* ---------- Cliques (botões, categorias, arquivos, planos) ---------- */
    form.addEventListener('click', function (e) {
        const proximo = e.target.closest('[data-passo-proximo]');
        if (proximo) {
            if (validarPasso(passoAtual)) irPara(Number(proximo.dataset.passoProximo));
            return;
        }

        const anterior = e.target.closest('[data-passo-anterior]');
        if (anterior) { irPara(Number(anterior.dataset.passoAnterior)); return; }

        const tag = e.target.closest('.categoria-tag');
        if (tag) { alternarCategoria(tag); return; }

        const abrir = e.target.closest('[data-arquivo-abrir]');
        if (abrir) { $('file-' + abrir.dataset.arquivoAbrir).click(); return; }

        const remover = e.target.closest('[data-arquivo-remover]');
        if (remover) { removerArquivo(remover.dataset.arquivoRemover); return; }

        const plano = e.target.closest('[data-plano]');
        if (plano) selecionarPlano(plano.dataset.plano);
    });

    // Teclado: Enter/Espaço nas caixas de upload e nos planos; Enter nos campos avança o passo
    form.addEventListener('keydown', function (e) {
        const caixa = e.target.closest('[data-arquivo-abrir], [data-plano]');
        if (caixa && (e.key === 'Enter' || e.key === ' ')) {
            e.preventDefault();
            caixa.click();
            return;
        }

        if (e.key === 'Enter' && e.target.matches('input:not([type=checkbox]):not([type=file])') && passoAtual < TOTAL_PASSOS) {
            e.preventDefault();
            const proximo = $('step' + passoAtual).querySelector('[data-passo-proximo]');
            if (proximo) proximo.click();
        }
    });

    // Tira o destaque de erro quando o campo é corrigido
    form.addEventListener('input', function (e) {
        if (e.target.classList.contains('is-invalid')) e.target.classList.remove('is-invalid');
    });

    /* ---------- Categorias ---------- */
    function alternarCategoria(tag) {
        const marcada = tag.classList.toggle('selected');
        tag.setAttribute('aria-pressed', marcada ? 'true' : 'false');

        const valores = Array.from(document.querySelectorAll('.categoria-tag.selected'))
            .map(function (t) { return t.dataset.value; });
        $('categorias').value = valores.join(',');
    }

    /* ---------- Documentos ---------- */
    form.addEventListener('change', function (e) {
        const input = e.target.closest('input[type=file][data-arquivo]');
        if (!input) return;

        const tipo = input.dataset.arquivo;
        const arquivo = input.files[0];
        if (!arquivo) return;

        if (arquivo.size > TAMANHO_MAX) {
            input.value = '';
            mostrarAviso('O arquivo é muito grande. Tamanho máximo: 5MB');
            return;
        }

        limparAviso();
        form.querySelector('[data-arquivo-abrir="' + tipo + '"]').classList.remove('is-invalid');
        $(tipo + '-name').textContent = arquivo.name;
        $(tipo + '-uploaded').classList.remove('d-none');
    });

    function removerArquivo(tipo) {
        $('file-' + tipo).value = '';
        $(tipo + '-uploaded').classList.add('d-none');
    }

    /* ---------- Plano ---------- */
    function selecionarPlano(plano) {
        document.querySelectorAll('.plano-card').forEach(function (card) {
            card.classList.toggle('selected', card.dataset.plano === plano);
        });
        $('plano').value = plano;
    }

    /* ---------- Máscaras ---------- */
    function mascara(id, limite, formatar) {
        $(id).addEventListener('input', function (e) {
            e.target.value = formatar(soDigitos(e.target.value).substring(0, limite));
        });
    }

    mascara('cpf', 11, function (v) {
        if (v.length > 9) return v.replace(/^(\d{3})(\d{3})(\d{3})(\d{1,2}).*/, '$1.$2.$3-$4');
        if (v.length > 6) return v.replace(/^(\d{3})(\d{3})(\d{1,3}).*/, '$1.$2.$3');
        if (v.length > 3) return v.replace(/^(\d{3})(\d{1,3}).*/, '$1.$2');
        return v;
    });

    mascara('cnpj', 14, function (v) {
        if (v.length > 12) return v.replace(/^(\d{2})(\d{3})(\d{3})(\d{4})(\d{1,2}).*/, '$1.$2.$3/$4-$5');
        if (v.length > 8) return v.replace(/^(\d{2})(\d{3})(\d{3})(\d{1,4}).*/, '$1.$2.$3/$4');
        if (v.length > 5) return v.replace(/^(\d{2})(\d{3})(\d{1,3}).*/, '$1.$2.$3');
        if (v.length > 2) return v.replace(/^(\d{2})(\d{1,3}).*/, '$1.$2');
        return v;
    });

    mascara('telefone', 11, function (v) {
        if (v.length > 10) return v.replace(/^(\d{2})(\d{5})(\d{1,4}).*/, '($1) $2-$3');
        if (v.length > 6) return v.replace(/^(\d{2})(\d{4})(\d{1,4}).*/, '($1) $2-$3');
        if (v.length > 2) return v.replace(/^(\d{2})(\d{1,4}).*/, '($1) $2');
        if (v.length > 0) return v.replace(/^(\d{1,2}).*/, '($1');
        return v;
    });

    /* ---------- Envio ---------- */
    // Envia só os campos de texto (os documentos ainda não vão para a API).
    function montarCorpo() {
        const corpo = new URLSearchParams();
        new FormData(form).forEach(function (valor, nome) {
            if (typeof valor === 'string') corpo.append(nome, valor);
        });
        return corpo;
    }

    let enviando = false;

    form.addEventListener('submit', function (e) {
        e.preventDefault();
        if (enviando) return;

        // Revalida os passos 1 a 3 (o envio pode ter vindo do teclado, de qualquer passo)
        for (let passo = 1; passo <= 3; passo++) {
            if (!validarPasso(passo)) {
                irPara(passo);
                validarPasso(passo); // mostra de novo o aviso, que irPara() limpou
                return;
            }
        }

        const botao = form.querySelector('.btn-submit');
        const textoBotao = botao.innerHTML;
        enviando = true;
        botao.disabled = true;
        botao.textContent = 'Enviando...';
        limparAviso();

        fetch(form.action, {
            method: 'POST',
            headers: { 'Accept': 'application/json' },
            body: montarCorpo()
        })
            .then(function (resposta) {
                return resposta.json()
                    .catch(function () { return { ok: false, mensagem: 'Resposta inesperada do servidor.' }; });
            })
            .then(function (resultado) {
                if (!resultado.ok) {
                    mostrarAviso(resultado.mensagem || 'Não foi possível concluir o cadastro.');
                    aviso.scrollIntoView({ behavior: 'smooth', block: 'center' });
                    return;
                }

                const planoCard = document.querySelector('.plano-card.selected');
                document.querySelector('[data-cadastro-nome]').textContent = $('nome').value.trim();
                document.querySelector('[data-cadastro-plano]').textContent = planoCard ? planoCard.dataset.planoNome : '';

                form.classList.add('d-none');
                $('indicador-passos').classList.add('d-none');
                limparAviso();
                $('cadastro-sucesso').classList.remove('d-none');
                $('cadastro-sucesso').scrollIntoView({ behavior: 'smooth', block: 'start' });
            })
            .catch(function () {
                mostrarAviso('Falha de conexão com o servidor. Tente novamente.');
            })
            .finally(function () {
                enviando = false;
                botao.disabled = false;
                botao.innerHTML = textoBotao;
            });
    });

    selecionarPlano('gratuito');
})();