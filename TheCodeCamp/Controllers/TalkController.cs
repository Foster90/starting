using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using TheCodeCamp.Data;
using TheCodeCamp.Models;

namespace TheCodeCamp.Controllers
{
    [RoutePrefix("api/camps/{moniker}/talks")]
    public class TalkController : ApiController

    {
        private readonly ICampRepository _repositiry;
        private readonly IMapper _mapper;

        public TalkController(ICampRepository repository, IMapper mapper)
        {
            _repositiry = repository;
            _mapper = mapper;

        }

        [Route()]
        public async Task<IHttpActionResult> Get(string moniker, bool includeSpeakers = false)
        {
            try 
            {
                var results = await _repositiry.GetTalksByMonikerAsync(moniker, includeSpeakers);

                return Ok(_mapper.Map<IEnumerable<TalkModel>>(results));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        [Route("{id:int}")]
        public async Task<IHttpActionResult> Get(string moniker,int id, bool includeSpeakers = false)
        {

            try 
            {
                var result = await _repositiry.GetTalkByMonikerAsync(moniker, id, includeSpeakers);
                if (result == null) return NotFound();

                return Ok(_mapper.Map<TalkModel>(result));
            }

            catch(Exception ex)
            {
                return InternalServerError(ex);

            }

        }

    }
}