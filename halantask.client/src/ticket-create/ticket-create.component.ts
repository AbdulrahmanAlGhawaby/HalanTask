import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { TicketService } from '../ticket.service/ticket.service';

@Component({
  selector: 'app-ticket-create',
  standalone: false,
  templateUrl: './ticket-create.component.html',
  styleUrls: ['./ticket-create.component.css']
})
export class TicketCreateComponent {
  ticketForm: FormGroup;
  successMessage: string = '';

  constructor(private fb: FormBuilder, private ticketService: TicketService) {
    this.ticketForm = this.fb.group({
      phoneNumber: ['', [Validators.required, Validators.pattern(/^[0-9]{10}$/)]],
      governorate: ['', Validators.required],
      city: ['', Validators.required],
      district: ['', Validators.required]
    });
  }

  submitForm() {
    if (this.ticketForm.valid) {
      this.ticketService.createTicket(this.ticketForm.value).subscribe({
        next: (response) => {
          this.successMessage = 'Ticket created successfully!';
          this.ticketForm.reset();
        },
        error: (err) => console.error('Error creating ticket:', err)
      });
    }
  }
}
