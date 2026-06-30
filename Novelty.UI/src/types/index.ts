// Book DTOs
interface BookSummaryDto {
  id: number
  title: string
  author: string
  averageRating: number
}
interface BookDetailDto {
  id: number
  title: string
  author: string
  reviews: ReviewDetailDto[]
  averageRating: number
}
interface CreateBookDto {
  title: string
  author: string
}
// User DTOs
interface UpdateUserDto {
  name: string
  email: string
}
interface UserDetailDto {
  id: number
  name: string
  email: string
  reviews: ReviewDetailDto[]
  favourites: FavouriteDetailDto[]
}
interface CreateUserDto {
  name: string
  email: string
}
// Review DTOs
interface ReviewDetailDto {
  id: number
  rating: number
  comment: string
  userName: string
  bookTitle: string
}
interface CreateReviewDto {
  rating: number
  comment: string
  userId: number
  bookId: number
}
// Favourite DTOs
interface FavouriteDetailDto {
  id: number
  userName: string
  bookSummary: BookSummaryDto
}
interface CreateFavouriteDto {
  userId: number
  bookId: number
}
