import { Component, OnInit } from '@angular/core';
import { NgbModule, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgbDropdownConfig } from '@ng-bootstrap/ng-bootstrap';
import { DisplayInterestsComponent } from '../../interests/display-interests/display-interests.component';
import { CreatePostComponent } from '../../posts/create-post/create-post.component';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
  providers: [NgbDropdownConfig],
  entryComponents: [DisplayInterestsComponent]
})
export class HeaderComponent implements OnInit {
  public isCollapsed = true;

  constructor(config: NgbDropdownConfig, private modalService: NgbModal) {
    config.placement = 'bottom-right';
    config.autoClose = true;
  }

  openInterestsModal() {
    const modalRef = this.modalService.open(DisplayInterestsComponent);
  }

  toggleMenu() {
    this.isCollapsed = !this.isCollapsed;
  }

  openCreatePostModal() {
    const modalRef = this.modalService.open(CreatePostComponent);
  }

  ngOnInit() {}
}
