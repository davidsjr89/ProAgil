using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.Model;

namespace WebAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        public readonly DataContext _dbcontext;
        public ValuesController(DataContext context){
            _dbcontext = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Evento>> GetAction()
        {
                
                return _dbcontext.Eventos.ToList();
        }
        
        [HttpGet("{id}")]
        public ActionResult<Evento> Get(int id){
            return new Evento[]{
                new Evento(){
                    EventoId = 1,
                    Tema = "Angular e .NET Core",
                    Local = "Belo Horizonte",
                    Lote = "1º Lote",
                    QtdPessoas  = 250,
                    DataEvento = DateTime.Now.AddDays(2).ToString("dd/MM/yyyy")
                },
                new Evento(){
                    EventoId = 2,
                    Tema = "Angular e Suas Novidades",
                    Local = "São Paulo",
                    Lote = "2º Lote",
                    QtdPessoas  = 450,
                    DataEvento = DateTime.Now.AddDays(5).ToString("dd/MM/yyyy")
                }
            }.FirstOrDefault(x => x.EventoId == id);
        }
    }
}