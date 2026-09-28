CREATE DATABASE IF NOT EXISTS db_escola_novo;
USE db_escola_novo;


CREATE TABLE IF NOT EXISTS tb_alunos (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    Matricula VARCHAR(50) NOT NULL,
    Curso VARCHAR(100) NOT NULL
);

-- -- ver alunos cadastrados: select * from tb_alunos; -- --