import { Component, OnInit } from '@angular/core';
import { TicketService } from '../ticket.service/ticket.service';
import { PaginatedList, Ticket } from '../models/ticket.model';

@Component({
  selector: 'app-ticket-list',
  standalone: false,
  templateUrl: './ticket-list.component.html',
  styleUrl: './ticket-list.component.css'
})
export class TicketListComponent implements OnInit {
  tickets: Ticket[] = []; 
  displayedColumns: string[] = ['id', 'phoneNumber', 'governorate', 'city', 'district', 'createdAt', 'isHandled', 'handle'];
  page = 0;
  pageSize = 2;
  totalPages = 0;
  totalTickets = 0;
  constructor(private ticketService: TicketService) { }

  ngOnInit() {
    this.loadTickets();
  }

  loadTickets() {
    this.ticketService.getTickets(this.page, this.pageSize).subscribe((data) => {
      this.tickets = data.items;
      this.page = data.pageIndex;
      this.totalPages = data.totalPages;
      this.totalTickets = data.totalCount;
    });
  }

  onPageChange(event: any) {
    this.page = event.pageIndex;
    this.pageSize = event.pageSize;
    this.loadTickets();
  }

  goToPage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.page = page;
      this.loadTickets();
    }
  }

  getTicketColor(createdAt: Date): string {
    const minutesOld = (new Date().getTime() - new Date(createdAt).getTime()) / 60000;

    if (minutesOld < 15) return 'yellow';
    if (minutesOld < 30) return 'green';
    if (minutesOld < 45) return 'blue';
    return 'red';
  }

  handleTicket(id: number) {
    this.ticketService.handleTicket(id).subscribe((data) => {
      this.loadTickets();
    });
  }
}

