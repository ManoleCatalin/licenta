import { Component, OnInit, Input } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DataStorageService } from '../../shared/data-storage.service';
import { Post } from '../post.model';
import { PreviewPostComponent } from '../preview-post/preview-post.component';
import { AuthService } from '../../auth/auth.service';


@Component({
  selector: 'app-post-card',
  templateUrl: './post-card.component.html',
  styleUrls: ['./post-card.component.css']
})
export class PostCardComponent implements OnInit {

  constructor(private modalService: NgbModal, private dataBaseSerivce: DataStorageService, private authService: AuthService) { }

  @Input() post: Post;
  ngOnInit() {
  }
  displayPreviewModal() {
    const modalRef = this.modalService.open(PreviewPostComponent, {size: 'lg', backdropClass: 'light-blue-backdrop'});
    modalRef.componentInstance.parent = this;
  }

  markLiked() {
    this.dataBaseSerivce.likePost(this.post.id, this.authService.getCurrentUserId()).subscribe(
      result => {
        this.post.likeId = result['id'];
        ++this.post.likesCount;
      }
    );
  }

  markFavorite() {

    this.dataBaseSerivce.favoritePost(this.post.id, this.authService.getCurrentUserId()).subscribe(
      result => {
        this.post.favoriteId = result['id'];
      }
    );
  }

  unmarkLiked() {
    this.dataBaseSerivce.unlikePost(this.post.likeId).subscribe(
      result => {
        this.post.likeId = null;
        --this.post.likesCount;
      }
    );
  }

  unmarkFavorite() {

    this.dataBaseSerivce.unfavoritePost(this.post.favoriteId).subscribe(
      result => {
        this.post.favoriteId = null;
      }
    );
  }
}
