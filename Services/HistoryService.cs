using Microsoft.SemanticKernel.ChatCompletion;

namespace SalesIA.Services;

public class HistoryService
{
    private readonly ChatHistory _chatHistory;

    public HistoryService()
    {
        _chatHistory = new ChatHistory();
        _chatHistory.AddSystemMessage("Vocé é um assistente pessoal que vai ajudar o usuário com as suas questões.");
    }

    public ChatHistory Get()
    {
        return _chatHistory;
    }

    public void SetHistoryMessage(AuthorRole role, string message) 
    {
        _chatHistory.AddMessage(role, message);
    }
}
