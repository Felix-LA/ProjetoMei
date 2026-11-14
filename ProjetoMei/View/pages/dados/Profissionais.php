<?php
/**
 * Profissionais em destaque (dados de exemplo).
 *
 * nota:      0 a 5, aceita meio ponto (4.5)
 * detalhe:   [rótulo, valor] da linha "Experiência: 8 anos"
 * info:      lista de [ícone Font Awesome, texto]
 * Troque por dados reais ou por uma chamada à API quando ela tiver esse recurso.
 */
return [
    [
        'nome' => 'Carlos Silva', 'titulo' => 'Marceneiro Profissional',
        'imagem' => 'https://images.unsplash.com/photo-1586023492125-27b2c045efd7?ixlib=rb-4.0.3&auto=format&fit=crop&w=800&h=600&q=80',
        'tags' => ['Móveis Sob Medida', 'Reformas', 'Instalação'],
        'nota' => 4.5, 'avaliacoes' => 127, 'preco' => 'R$ 80 - R$ 150 / hora',
        'detalhe' => ['Experiência', '8 anos'],
        'info' => [['fa-location-dot', 'São Paulo - SP'], ['fa-briefcase', '245 projetos']],
    ],
    [
        'nome' => 'Ana Costa', 'titulo' => 'Desenvolvedora Web Full Stack',
        'imagem' => 'https://images.unsplash.com/photo-1575089976121-8ed7b2a54265?ixlib=rb-4.0.3&auto=format&fit=crop&w=800&h=600&q=80',
        'tags' => ['Sites', 'Sistemas Web', 'WordPress'],
        'nota' => 5, 'avaliacoes' => 89, 'preco' => 'R$ 100 - R$ 200 / hora',
        'detalhe' => ['Tecnologias', 'React, Node.js, PHP'],
        'info' => [['fa-globe', 'Remoto'], ['fa-briefcase', '156 projetos']],
    ],
    [
        'nome' => 'Roberto Santos', 'titulo' => 'Eletricista Residencial e Industrial',
        'imagem' => 'https://images.unsplash.com/photo-1621905252507-b35492cc74b4?ixlib=rb-4.0.3&auto=format&fit=crop&w=800&h=600&q=80',
        'tags' => ['Instalações', 'Manutenção', 'Laudos'],
        'nota' => 4, 'avaliacoes' => 203, 'preco' => 'R$ 70 - R$ 120 / hora',
        'detalhe' => ['Certificado', 'CREA/CRC'],
        'info' => [['fa-location-dot', 'Rio de Janeiro - RJ'], ['fa-briefcase', '12 anos experiência']],
    ],
    [
        'nome' => 'Fernanda Lima', 'titulo' => 'Designer Gráfica e Social Media',
        'imagem' => 'https://images.unsplash.com/photo-1565106430482-8f6e74349ca1?ixlib=rb-4.0.3&auto=format&fit=crop&w=800&h=600&q=80',
        'tags' => ['Logotipos', 'Posts', 'Identidade Visual'],
        'nota' => 5, 'avaliacoes' => 142, 'preco' => 'R$ 60 - R$ 150 / projeto',
        'detalhe' => ['Especialidade', 'Branding Digital'],
        'info' => [['fa-location-dot', 'Belo Horizonte - MG'], ['fa-briefcase', '5 anos experiência']],
    ],
    [
        'nome' => 'José Almeida', 'titulo' => 'Contador Especializado em MEI',
        'imagem' => 'https://images.unsplash.com/photo-1554224155-6726b3ff858f?ixlib=rb-4.0.3&auto=format&fit=crop&w=800&h=600&q=80',
        'tags' => ['Declarações', 'Abertura', 'Consultoria'],
        'nota' => 4.5, 'avaliacoes' => 98, 'preco' => 'R$ 150 - R$ 300 / mês',
        'detalhe' => ['Especialidade', 'MEIs e Microempresas'],
        'info' => [['fa-location-dot', 'Porto Alegre - RS'], ['fa-briefcase', 'CRC Ativo']],
    ],
    [
        'nome' => 'Mariana Oliveira', 'titulo' => 'Fotógrafa Profissional',
        'imagem' => 'https://images.unsplash.com/photo-1554048612-b6a482bc67e5?ixlib=rb-4.0.3&auto=format&fit=crop&w=800&h=600&q=80',
        'tags' => ['Casamentos', 'Produtos', 'Retratos'],
        'nota' => 5, 'avaliacoes' => 167, 'preco' => 'R$ 300 - R$ 1.500 / evento',
        'detalhe' => ['Equipamento', 'Canon Full Frame'],
        'info' => [['fa-location-dot', 'Salvador - BA'], ['fa-briefcase', '7 anos experiência']],
    ],
];