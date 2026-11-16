<?php
/**
 * Mapa de rotas do front: nome da rota => arquivo dentro da pasta View.
 *
 * Para trocar o endereço de uma página, mude só aqui: todos os links e
 * redirecionamentos usam rota('nome') em vez de escrever o caminho.
 *
 * Rotas marcadas como "A CRIAR" ainda não têm arquivo em pages/.
 */
return [
    // ---- Vitrine ----
    'inicio'         => '/pages/Inicio.php',
    'categorias'     => '/pages/Categoria.php',
    'profissionais'  => '/pages/Listagem.php',
    'contato'        => '/pages/Contato.php',
    'cadastro-mei'   => '/pages/CadastroMei.php',
    'cadastro-mei.salvar' => '/pages/CadastroMeiSalvar.php',
    'como-funciona'  => '/pages/ComoFunciona.php',   // A CRIAR
    'termos'         => '/pages/Termos.php',          // A CRIAR
    'privacidade'    => '/pages/Privacidade.php',     // A CRIAR

    // ---- Login ----
    'login'          => '/pages/Login.php',
    'autenticar'     => '/pages/Autenticar.php',

    // ---- Usuários ----
    'usuarios'        => '/pages/usuario/Listar.php',
    'usuario.salvar'  => '/pages/usuario/Salvar.php',
    'usuario.excluir' => '/pages/usuario/Excluir.php',
];