import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-preview-post',
  templateUrl: './preview-post.component.html',
  styleUrls: ['./preview-post.component.css']
})
export class PreviewPostComponent implements OnInit {

  @Input() title;
  @Input() imageUrl;
  @Input() postUrl;

  constructor(public activeModal: NgbActiveModal) {}

  ngOnInit() {
  }

}
