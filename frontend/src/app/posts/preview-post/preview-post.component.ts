import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { PostCardComponent } from '../post-card/post-card.component';
import { CommentsComponent} from '../../comments/comments.component';

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
