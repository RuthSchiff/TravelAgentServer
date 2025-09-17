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

            try
            {
                // הדפסה ללוגים לפני קריאה לפונקציה
                Console.WriteLine("Received message from client: " + request.Message);

                var response = await _conversationManager.GetResponseAsync(request.Message);

                // הדפסה ללוגים של התשובה שהתקבלה
                Console.WriteLine("Response from GetResponseAsync: " + (response ?? "null"));

                // בדיקה אם התשובה ריקה או לא תקינה
                if (string.IsNullOrEmpty(response))
                {
                    return Ok(new { Response = "השרת החזיר תשובה ריקה." });
                }

                return Ok(new { Response = response });
            }
            catch (Exception ex)
            {
                // הדפסת פרטי השגיאה המלאים ללוגים
                Console.WriteLine($"[Error]: An exception occurred. Details: {ex.ToString()}");
                return BadRequest("אירעה שגיאה בשרת. אנא נסה שוב.");
            }
        }
    }
}
