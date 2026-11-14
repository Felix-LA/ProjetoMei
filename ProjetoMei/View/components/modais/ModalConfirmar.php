<?php /** Modal de confirmação de exclusão. */ ?>
<div class="modal" id="modal-confirmar" aria-hidden="true" role="dialog" aria-labelledby="modal-confirmar-titulo">
    <div class="modal__caixa modal__caixa--pequena">
        <div class="modal__topo">
            <h2 id="modal-confirmar-titulo">Excluir usuário</h2>
            <button type="button" class="modal__x" data-modal-fechar aria-label="Fechar">&times;</button>
        </div>

        <p>Tem certeza que deseja excluir <strong data-nome-excluir></strong>? Esta ação não pode ser desfeita.</p>

        <form method="post" action="<?= Rotas('usuario.excluir') ?>">
            <input type="hidden" name="csrf" value="<?= h(csrf_token()) ?>">
            <input type="hidden" name="id" id="excluir-id" value="">

            <div class="modal__rodape">
                <button type="button" class="btn btn--claro" data-modal-fechar>Cancelar</button>
                <button type="submit" class="btn btn--perigo">Excluir</button>
            </div>
        </form>
    </div>
</div>