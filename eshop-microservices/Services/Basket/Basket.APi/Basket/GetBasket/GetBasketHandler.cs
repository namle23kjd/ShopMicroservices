
namespace Basket.APi.Basket.GetBasket;

//Hàm này là hàm khi chúng ta muốn nhaaoj vào 1 dữ lieejk gì đó nó sữ dùng dữ lieuejd dó đẻ mà xử lí cũng như insert hoạc tìm kiếm , delete

public record GetBasketQuery(string UserName) :  IQuery<GetBasketResult>;

//Hàm này là dự vào yêu cầu đầu vào của mình và mong muốn trả về giá trị
public record GetBasketResult(ShoppingCart Cart);

public class GetBasketQueryHandler (IBasketRepository repository)
    : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        var basket = await repository.GetBasket(query.UserName);


        return new GetBasketResult(basket);

    }
}
