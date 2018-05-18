import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DisplayInterestsComponent } from './display-interests/display-interests.component';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [DisplayInterestsComponent],
  exports: [DisplayInterestsComponent],
  entryComponents: [DisplayInterestsComponent]
})
export class InterestsModule { }
