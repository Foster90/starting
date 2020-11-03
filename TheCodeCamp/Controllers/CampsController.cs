using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
//using System.Web.Mvc;
using TheCodeCamp.Data;
using TheCodeCamp.Data.Migrations;

namespace TheCodeCamp.Controllers
{

    [RoutePrefix("api/camps")]
    public class CampsController : ApiController
    {
        private readonly ICampRepository _repository;
        private readonly IMapper _mapper;


        public CampsController(ICampRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [Route()]
        public async Task<IHttpActionResult> Get(bool includeTalks = false)
        {
            try
            {
                var result = await _repository.GetAllCampsAsync(includeTalks);

                var mappedResult = _mapper.Map<IEnumerable<CampModel>>(result);

                return Ok(mappedResult);
            }

            catch(Exception ex)            
            {
                //TODO Add Logging
                return InternalServerError(ex);
            }
        }

        [Route("{moniker}")]
        public async Task<IHttpActionResult> Get(string moniker)
        {
            try 
            {
                var result = await _repository.GetCampAsync(moniker);
                if (result == null) return NotFound();

                return Ok(_mapper.Map<CampModel>(result));
            }

            catch (Exception ex)
            { 
                return InternalServerError(ex); 
            }

        }

    }
}