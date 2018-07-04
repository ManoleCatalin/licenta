import { Component, Input, OnInit } from '@angular/core';
import { PostCardComponent } from '../post-card/post-card.component';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-preview-post',
  templateUrl: './preview-post.component.html',
  styleUrls: ['./preview-post.component.css']
})
export class PreviewPostComponent implements OnInit {

  @Input() parent: PostCardComponent;

  constructor(public activeModal: NgbActiveModal) {}

  ngOnInit() {
  }

}
