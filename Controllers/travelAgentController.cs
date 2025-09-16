using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using travelAgent.classes;

namespace travelAgent.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly ConversationManager _conversationManager;

        public ChatController(ConversationManager conversationManager)
        {
            _conversationManager = conversationManager;
        }

        [HttpPost("message")]
        public async Task<IActionResult> PostMessage([FromBody] ChatRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Message))
            {
                return BadRequest("הודעה לא תקינה.");
            }

            var response = await _conversationManager.GetResponseAsync(request.Message);
            return Ok(new { Response = response });
        }
    }
}
