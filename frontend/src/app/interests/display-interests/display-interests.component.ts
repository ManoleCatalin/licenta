import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-display-interests',
  templateUrl: './display-interests.component.html',
  styleUrls: ['./display-interests.component.css']
})
export class DisplayInterestsComponent implements OnInit {

  @Input() name;

  constructor(public activeModal: NgbActiveModal) {}

  ngOnInit() {
  }

}
