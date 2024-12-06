import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ListingModel } from '../../services/listings.service';

@Component({
  selector: 'app-listing-details',
  templateUrl: './listing-details.component.html',
  styleUrls: ['./listing-details.component.css'],
})
export class ListingDetailsComponent implements OnInit {
  listing: ListingModel | undefined;
  listings: ListingModel[] = [];
  constructor(private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.listings = history.state.listings || [];
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.listing = this.listings.find((l) => l.Id === id);
  }
}
