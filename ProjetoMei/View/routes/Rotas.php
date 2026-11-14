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
    'inicio'         => '/pages/inicio.php',
    'categorias'     => '/pages/categorias.php',      // A CRIAR
    'profissionais'  => '/pages/profissionais.php',   // A CRIAR
    'contato'        => '/pages/contato.php',         // A CRIAR
    'cadastro-mei'   => '/pages/cadastro-mei.php',    // A CRIAR
    'como-funciona'  => '/pages/como-funciona.php',   // A CRIAR
    'termos'         => '/pages/termos.php',          // A CRIAR
    'privacidade'    => '/pages/privacidade.php',     // A CRIAR

    // ---- Login ----
    'login'          => '/pages/login.php',
    'autenticar'     => '/pages/autenticar.php',

    // ---- Usuários ----
    'usuarios'        => '/pages/usuario/listar.php',
    'usuario.salvar'  => '/pages/usuario/salvar.php',
    'usuario.excluir' => '/pages/usuario/excluir.php',
];