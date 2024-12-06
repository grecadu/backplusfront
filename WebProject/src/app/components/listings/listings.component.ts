import { Component, OnInit } from '@angular/core';
import { ListingsService } from '../../services/listings.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-listings',
  templateUrl: './listings.component.html',
  styleUrls: ['./listings.component.css']
})
export class ListingsComponent implements OnInit {
  listings: any[] = [];
  filteredListings: any[] = [];
  filters = {
    bedrooms: 0,
    bathrooms: 0,
    parking: 0,
    priceRange: { min: 0, max: 1000000 }
  };

  constructor(private listingsService: ListingsService, private router: Router) { }

  ngOnInit(): void {
    this.listingsService.getListings().subscribe((data) => {
      console.log(data)
      this.listings = data;
      this.filteredListings = data;
    });
  }

  applyFilters() {
    this.filteredListings = this.listings.filter(listing =>
      listing.Bedrooms >= this.filters.bedrooms &&
      listing.Bathrooms >= this.filters.bathrooms &&
      listing.Parking >= this.filters.parking &&
      listing["Sale Price"] >= this.filters.priceRange.min &&
      listing["Sale Price"] <= this.filters.priceRange.max
    );
  }

  viewDetails(id: number): void {
    this.router.navigate(['/listings', id], {
      state: { listings: this.listings },
    });
  }
}
