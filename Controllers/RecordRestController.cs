using Microsoft.AspNetCore.Mvc;
using notebook.Services;
using notebook.Dto;
using notebook.Model;
using Microsoft.AspNetCore.Authorization;

namespace notebook.Controllers;

[ApiController]
[Route("base/rest/profile/records")]
public class RecordRestController : ControllerBase
{
    private readonly ILogger<RecordRestController> _logger;

    private IRecordService<Record> _recordService;

    public RecordRestController(
        ILogger<RecordRestController> logger,
        IRecordService<Record> recordService
    )
    {
        _logger = logger;
        _recordService = recordService;
    }
    
    [Authorize]
    [HttpGet]
    public IEnumerable<Record> Get()
    {
        return _recordService.Get();
    }
    [Authorize]
    [HttpPost]
    public void Post([FromBody] RecordDto recordDto)
    {
        Record record = new Record {id = null, description = recordDto.description, dateTime = DateTime.Now};
        _recordService.Create(record);
    }
    [Authorize]
    [Route("{id}")]
    [HttpPut]
    public void Put([FromBody] RecordDto recordDto)
    {   
        int id = recordDto.id ?? 0;
        if (id == 0) return;
        Record record = _recordService.FindById(id);
        record.description = recordDto.description;
        _recordService.Create(record);
    }
    [Authorize]
    [Route("{id:int}")]
    [HttpDelete]
    public void Delete(int id)
    {   
        Record record = _recordService.FindById(id);
        _recordService.Remove(record);
    }
}
