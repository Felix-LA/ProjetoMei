<?php /** Modal de cadastro/edição de usuário. O JS (usuario.js) preenche os campos ao editar. */ ?>
<div class="modal" id="modal-usuario" aria-hidden="true" role="dialog" aria-labelledby="modal-usuario-titulo">
    <div class="modal__caixa">
        <div class="modal__topo">
            <h2 id="modal-usuario-titulo" data-titulo>Novo usuário</h2>
            <button type="button" class="modal__x" data-modal-fechar aria-label="Fechar">&times;</button>
        </div>

        <form method="post" action="<?= Rotas('usuario.salvar') ?>" id="form-usuario" novalidate>
            <input type="hidden" name="csrf" value="<?= h(csrf_token()) ?>">
            <input type="hidden" name="id" id="usuario-id" value="">

            <div class="campo">
                <label for="usuario-nome">Nome</label>
                <input type="text" id="usuario-nome" name="nome" maxlength="50" required autocomplete="off">
                <small class="campo__erro" data-erro hidden>Informe o nome (máx. 50 caracteres).</small>
            </div>

            <div class="modal__rodape">
                <button type="button" class="btn btn--claro" data-modal-fechar>Cancelar</button>
                <button type="submit" class="btn">Salvar</button>
            </div>
        </form>
    </div>
</div>