import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TicketCreateComponent } from '../ticket-create/ticket-create.component';
import { TicketListComponent } from '../ticket-list/ticket-list.component';

const routes: Routes = [
  { path: '', redirectTo: '/tickets', pathMatch: 'full' }, // Default route
  { path: 'tickets', component: TicketListComponent },
  { path: 'tickets/create', component: TicketCreateComponent, pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
