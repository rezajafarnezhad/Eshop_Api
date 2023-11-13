using Common.Application;

namespace Shop.Application.Comments.Create
{
    public record CreateCommentCommand(string Text, long UserId, long ProductId) : IBaseCommand<long>;
}