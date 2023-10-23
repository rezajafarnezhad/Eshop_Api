using Common.Application;
using Shop.Domain.CommentAgg;

namespace Shop.Application.Comments.Create
{
    public class CreateCommentCommandHandler : IBaseCommandHandler<CreateCommentCommand,long>
    {
        private readonly ICommentRepository _repository;

        public CreateCommentCommandHandler(ICommentRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult<long>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = new Comment(request.UserId, request.ProductId, request.Text);
             _repository.Add(comment);
            await _repository.Save();
            return OperationResult<long>.Success(comment.Id);
        }
    }
}