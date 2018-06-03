import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import { DataStorageService } from '../../shared/data-storage.service';
import { Interest } from '../interest.model';

@Component({
  selector: 'app-display-interests',
  templateUrl: './display-interests.component.html',
  styleUrls: ['./display-interests.component.css']
})
export class DisplayInterestsComponent implements OnInit {
  @Input() name;

  public searchedInterest: Interest;
  public imgThumbnailSelected = '';
  public displayInterestName = '';
  public activeInterests: Interest[] = [];

  constructor(public activeModal: NgbActiveModal, private dataStorageService: DataStorageService) {

    this.activeInterests = dataStorageService.getActiveInterests(1, 5);
  }

  ngOnInit() {}

  setPhoto($event) {
    const interests = this.dataStorageService.getAvailableInterests().filter(interest => {
       return interest.name === $event.item;
    });

    console.log(interests.length);

    if (interests.length === 1) {
      this.imgThumbnailSelected = interests[0].thumbnailImgUrl;

      this.searchedInterest = interests[0];

      this.displayInterestName = this.searchedInterest.name;
    }
  }

  search = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(200),
      distinctUntilChanged(),
      map(term => {
        return term.length < 2
          ? []
          : this.dataStorageService
              .getAvailableInterests()
              .filter(v => v.name.toLowerCase().indexOf(term.toLowerCase()) > -1)
              .map(v => v.name)
              .slice(0, 10);
      })
    )

  addInterest() {
    console.log(this.searchedInterest);
    this.dataStorageService.addActiveInterest(this.searchedInterest.id);
    this.activeInterests = this.dataStorageService.getActiveInterests(1, 5);
    console.log(this.activeInterests);
  }
}
