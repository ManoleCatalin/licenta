import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DisplayInterestsComponent } from './display-interests/display-interests.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';


@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    NgbModule,
  ],
  declarations: [DisplayInterestsComponent],
  exports: [DisplayInterestsComponent],
  entryComponents: [DisplayInterestsComponent]
})
export class InterestsModule { }
