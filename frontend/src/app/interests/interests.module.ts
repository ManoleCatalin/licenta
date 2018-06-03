import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DisplayInterestsComponent } from './display-interests/display-interests.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CommentsModule } from '../comments/comments.module';


@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    NgbModule,
    CommentsModule
  ],
  declarations: [DisplayInterestsComponent,
],
  exports: [DisplayInterestsComponent],
  entryComponents: [DisplayInterestsComponent]
})
export class InterestsModule { }
