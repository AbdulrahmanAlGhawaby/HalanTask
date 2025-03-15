using HalanTask.Domain.Models;
using HalanTask.Domain.Utilities;

namespace HalanTask.Infrastructure.Repositories
{
    public interface ITicketRepository
    {
        Task AddAsync(Ticket ticket);
        Task<PaginatedList<Ticket>> GetPaginatedTickets(int page, int pageSize);
        Task<Ticket?> GetTicket(int id);
        Task<List<Ticket>> GetUnHandledTicketsOlderThan(int minutes);
        Task HandleTicket(int Id);
        Task MarkAsHandledAsync(List<Ticket> tickets);

    }
}