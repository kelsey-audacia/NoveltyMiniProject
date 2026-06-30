// Book DTOs
export interface BookSummaryDto {
  id: number
  title: string
  author: string
  averageRating: number
}
export interface BookDetailDto {
  id: number
  title: string
  author: string
  reviews: ReviewDetailDto[]
  averageRating: number
}
export interface CreateBookDto {
  title: string
  author: string
}
// User DTOs
export interface UpdateUserDto {
  name: string
  email: string
}
export interface UserDetailDto {
  id: number
  name: string
  email: string
  reviews: ReviewDetailDto[]
  favourites: FavouriteDetailDto[]
}
export interface CreateUserDto {
  name: string
  email: string
}
// Review DTOs
export interface ReviewDetailDto {
  id: number
  rating: number
  comment: string
  userName: string
  bookTitle: string
}
export interface CreateReviewDto {
  rating: number
  comment: string
  userId: number
  bookId: number
}
// Favourite DTOs
export interface FavouriteDetailDto {
  id: number
  userName: string
  bookSummary: BookSummaryDto
}
export interface CreateFavouriteDto {
  userId: number
  bookId: number
}
