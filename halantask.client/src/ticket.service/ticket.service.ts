import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PaginatedList, Ticket } from '../models/ticket.model';

@Injectable({ providedIn: 'root' })
export class TicketService {
  private apiUrl = 'https://localhost:7055/api/Ticket';

  constructor(private http: HttpClient) { }

  getTickets(page: number, pageSize: number): Observable<PaginatedList<Ticket>> {
    return this.http.get<PaginatedList<Ticket>>(`${this.apiUrl}/GetTickets?page=${page}&pageSize=${pageSize}`);
  }

  createTicket(ticket: Ticket): Observable<string> {
    return this.http.post<string>(`${this.apiUrl}/CreateTicket`, ticket);
  }

  handleTicket(ticketId: number): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/HandleTicket?Id=${ticketId}`, null);
  }
}

