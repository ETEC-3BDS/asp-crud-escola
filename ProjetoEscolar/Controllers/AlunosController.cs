using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ProjetoEscolar.Models;

namespace ProjetoEscolar.Controllers
{
    public class AlunosController : Controller
    {
        
        private readonly string _connectionString = "server=localhost;port=3305;database=db_escola_novo;user=root;password=12345678;";

        
        public IActionResult Index()
        {
            List<Aluno> listaAlunos = new List<Aluno>();

            using (var conexao = new MySqlConnection(_connectionString))
            {
                conexao.Open();
                string query = "SELECT Id, Nome, Matricula, Curso FROM tb_alunos";
                using (var cmd = new MySqlCommand(query, conexao))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listaAlunos.Add(new Aluno
                            {
                                Id = reader.GetInt32("Id"),
                                Nome = reader.GetString("Nome"),
                                Matricula = reader.GetString("Matricula"),
                                Curso = reader.GetString("Curso")
                            });
                        }
                    }
                }
            }

            return View(listaAlunos);
        }

        
        public IActionResult CadastrarAluno()
        {
            return View();
        }

        
        [HttpPost]
        public IActionResult CadastrarAluno(Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                using (var conexao = new MySqlConnection(_connectionString))
                {
                    conexao.Open();
                    string query = "INSERT INTO tb_alunos (Nome, Matricula, Curso) VALUES (@Nome, @Matricula, @Curso)";
                    using (var cmd = new MySqlCommand(query, conexao))
                    {
                        cmd.Parameters.AddWithValue("@Nome", aluno.Nome);
                        cmd.Parameters.AddWithValue("@Matricula", aluno.Matricula);
                        cmd.Parameters.AddWithValue("@Curso", aluno.Curso);
                        cmd.ExecuteNonQuery();
                    }
                }

                TempData["Mensagem"] = "Aluno cadastrado com sucesso!";
                return RedirectToAction("Index");
            }

            return View(aluno);
        }

        
        public IActionResult Edit(int id)
        {
            Aluno aluno = null;

            using (var conexao = new MySqlConnection(_connectionString))
            {
                conexao.Open();
                string query = "SELECT Id, Nome, Matricula, Curso FROM tb_alunos WHERE Id = @Id";
                using (var cmd = new MySqlCommand(query, conexao))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            aluno = new Aluno
                            {
                                Id = reader.GetInt32("Id"),
                                Nome = reader.GetString("Nome"),
                                Matricula = reader.GetString("Matricula"),
                                Curso = reader.GetString("Curso")
                            };
                        }
                    }
                }
            }

            if (aluno == null) return NotFound();

            return View(aluno);
        }

        
        [HttpPost]
        public IActionResult Edit(Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                using (var conexao = new MySqlConnection(_connectionString))
                {
                    conexao.Open();
                    string query = "UPDATE tb_alunos SET Nome=@Nome, Matricula=@Matricula, Curso=@Curso WHERE Id=@Id";
                    using (var cmd = new MySqlCommand(query, conexao))
                    {
                        cmd.Parameters.AddWithValue("@Id", aluno.Id);
                        cmd.Parameters.AddWithValue("@Nome", aluno.Nome);
                        cmd.Parameters.AddWithValue("@Matricula", aluno.Matricula);
                        cmd.Parameters.AddWithValue("@Curso", aluno.Curso);
                        cmd.ExecuteNonQuery();
                    }
                }

                TempData["Mensagem"] = "Aluno atualizado com sucesso!";
                return RedirectToAction("Index");
            }

            return View(aluno);
        }

        
        [HttpPost]
        public IActionResult Delete(int id)
        {
            using (var conexao = new MySqlConnection(_connectionString))
            {
                conexao.Open();
                string query = "DELETE FROM tb_alunos WHERE Id = @Id";
                using (var cmd = new MySqlCommand(query, conexao))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }

            TempData["Mensagem"] = "Aluno excluído com sucesso!";
            return RedirectToAction("Index");
        }
    }
}