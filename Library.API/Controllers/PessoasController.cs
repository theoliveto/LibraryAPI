using Library.Domain.Entities;
using Library.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Library.Domain.DTOs.Pessoas;

namespace Library.API.Controllers { 
    public class PessoasController : ApiControllerBase<Pessoas, PostRequest, PutRequest> {
        private readonly IPessoasService _service;

        public PessoasController(IPessoasService service) : base(service) {
            _service = service;
        }

        [HttpGet("verificar-cpf/{cpf}")]
        public async Task<bool> VerificarCpfAsync(string cpf, int? cdPessoa) {
            return await _service.VerificarCpfAsync(cpf, cdPessoa);
        }

        [HttpGet("verificar-email/{email}")]
        public async Task<bool> VerificarEmailAsync(string email, int? cdPessoa) {
            return await _service.VerificarEmailAsync(email, cdPessoa);
        }
    }
}