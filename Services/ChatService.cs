using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace SalesIA.Services;

public class ChatService
{
    private readonly Kernel _kernel;
    private readonly IChatCompletionService _chatCompletionService;
    private readonly HistoryService _historyService;

    public ChatService(Kernel kernel,
                       IChatCompletionService chatCompletionService,
                       HistoryService historyService)
    {
        _kernel = kernel;
        _chatCompletionService = chatCompletionService;
        _historyService = historyService;
    }

    public async Task<string?> GetResponse(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
        { 
            return null;
        }

        var history = _historyService.Get();
        _historyService.SetHistoryMessage(AuthorRole.User, message);

        var openAIPromptExecutionSettings = new OpenAIPromptExecutionSettings()
        {
            ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
        };

        var response = await _chatCompletionService.GetChatMessageContentAsync(history,
                                                                               openAIPromptExecutionSettings,
                                                                               _kernel);
        var responseContent = response.Content ?? string.Empty;
        _historyService.SetHistoryMessage(response.Role, responseContent);

        return responseContent;
    }
}
