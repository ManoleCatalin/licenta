import { Component, OnInit, Input } from '@angular/core';
import { Post } from '../post.model';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { PreviewPostComponent } from '../preview-post/preview-post.component';

@Component({
  selector: 'app-post-card',
  templateUrl: './post-card.component.html',
  styleUrls: ['./post-card.component.css']
})
export class PostCardComponent implements OnInit {

  constructor(private modalService: NgbModal) { }

  @Input() post: Post;
  ngOnInit() {
  }
  displayPreviewModal() {
    const modalRef = this.modalService.open(PreviewPostComponent, {size: 'lg', backdropClass: 'light-blue-backdrop'});
    modalRef.componentInstance.parent = this;
  }

  markLiked() {
    this.post.liked = true;
  }

  markFavorite() {
    this.post.favorite = true;
  }
}
