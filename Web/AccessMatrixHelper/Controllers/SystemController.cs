using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DAM.Model;

namespace AccessMatrixHelper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemController : ControllerBase
    {
        [HttpPost("Parse")]
        public async Task<IActionResult> Parse([FromBody] DAM.Model.Input input)
        {
            try { await AccessMatrixHelper.DB.Method.MySQLMethod.LogNewConnection("System/Parse"); } catch {}

            try
            {
                return Ok(DAM.Method.SystemMethod.Parse(input.text));
            }
            catch (DAM.Model.DACException ex)
            {
                return StatusCode(418, ex);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("AccessMatrix")]
        public async Task<IActionResult> AccessMatrix([FromBody] DAM.Model.System system)
        {
            try { await AccessMatrixHelper.DB.Method.MySQLMethod.LogNewConnection("System/AccessMatrix"); } catch {}

            try
            {
                return Ok(DAM.Method.SystemMethod.GetAccessMatrix(system));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        
        [HttpGet("TabSeparators")]
        public async Task<IActionResult> TabSeparators()
        {
            try { await AccessMatrixHelper.DB.Method.MySQLMethod.LogNewConnection("System/TabSeparators"); } catch {}

            try
            {
                return Ok(DAM.Dict.CommonDict.TabAfterSeparators);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        [HttpGet("NLSeparators")]
        public async Task<IActionResult> NLSeparators()
        {
            try { await AccessMatrixHelper.DB.Method.MySQLMethod.LogNewConnection("System/NLSeparators"); } catch {}

            try
            {
                return Ok(DAM.Dict.CommonDict.NewLineAfterSeparators);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        [HttpGet("Separators")]
        public async Task<IActionResult> Separators()
        {
            try { await AccessMatrixHelper.DB.Method.MySQLMethod.LogNewConnection("System/Separators"); } catch {}

            try
            {
                return Ok(DAM.Dict.CommonDict.Separators);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        [HttpGet("CorrectExample")]
        public async Task<IActionResult> CorrectExample()
        {
            try { await AccessMatrixHelper.DB.Method.MySQLMethod.LogNewConnection("System/CorrectExample"); } catch {}

            try
            {
                return Ok(DAM.Dict.Example.Correct);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        [HttpGet("InCorrectExample")]
        public async Task<IActionResult> InCorrectExample()
        {
            try { await AccessMatrixHelper.DB.Method.MySQLMethod.LogNewConnection("System/InCorrectExample"); } 
            
            catch(Exception ex) 
            {
            }    

            try
            {
                return Ok(DAM.Dict.Example.InCorrect);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        
        [HttpPost("SaveSystem")]
        public async Task<IActionResult> SaveSystem([FromBody] DAM.Model.System system)
        {
            try { await AccessMatrixHelper.DB.Method.MySQLMethod.LogNewConnection("System/SaveSystem"); } catch {}

            try
            {
                await AccessMatrixHelper.DB.Method.MySQLMethod.SaveSystem(system);
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
        }





        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        // // GET api/values/5
        // [HttpGet("{id}")]
        // public ActionResult<string> Get(int id)
        // {
        //     return "value";
        // }

        // // POST api/values
        // [HttpPost]
        // public void Post([FromBody] string value)
        // {
        // }

        // // PUT api/values/5
        // [HttpPut("{id}")]
        // public void Put(int id, [FromBody] string value)
        // {
        // }

        // // DELETE api/values/5
        // [HttpDelete("{id}")]
        // public void Delete(int id)
        // {
        // }
    }
}
