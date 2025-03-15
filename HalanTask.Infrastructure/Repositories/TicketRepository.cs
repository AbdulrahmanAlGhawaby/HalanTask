using HalanTask.Domain.Models;
using HalanTask.Domain.Utilities;
using HalanTask.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalanTask.Infrastructure.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly ApplicationDbContext _context;
        public TicketRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task<PaginatedList<Ticket>> GetPaginatedTickets(int page, int pageSize)
        {
            var query = _context.Tickets.OrderByDescending(t => t.CreatedAt);
            var count = await query.CountAsync();
            var items = await query.Skip((page) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<Ticket>(items, count, page, pageSize);
        }
        public async Task<Ticket?> GetTicket(int id)
        {
            return await _context.Tickets.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task HandleTicket(int Id)
        {
            var ticket = await _context.Tickets.FindAsync(Id);

            if (ticket != null)
            {
                ticket.IsHandled = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Ticket>> GetUnHandledTicketsOlderThan(int minutes)
        {
            return await _context.Tickets.Where(t => DateTime.Now >= t.CreatedAt.AddMinutes(minutes))
                                         .ToListAsync();
        }

        public async Task MarkAsHandledAsync(List<Ticket> tickets)
        {
            foreach (var ticket in tickets)
            {
                _context.Attach(ticket);
                ticket.IsHandled = true;
            }
            await _context.SaveChangesAsync();

        }
    }

}
