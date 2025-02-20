﻿using library_management_system.Database;
using library_management_system.Database.Entiy;
using library_management_system.Database.Entiy.ReviewEntitys;
using library_management_system.DTOs;
using library_management_system.DTOs.LikeandReview;
using library_management_system.Repositories;

namespace library_management_system.Services
{
    public class ReviewService
    {
        private readonly ReviewRepository _reviewRepository;

        public ReviewService(ReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        // Add Normal Book Review
        public async Task<ApiResponse<bool>> AddNormalBookReviewAsync(NormalBookReview review)
        {
            var response = new ApiResponse<bool>();

            try
            {
                if (review == null)
                {
                    response.Success = false;
                    response.Message = "Review cannot be null.";
                    return response;
                }

                var isAdded = await _reviewRepository.AddNormalBookReviewAsync(review);
                if (!isAdded)
                {
                    response.Success = false;
                    response.Message = "Failed to add the review.";
                    return response;
                }

                response.Success = true;
                response.Message = "Review added successfully.";
                response.Data = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while adding the review.";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        // Add Ebook Review
        public async Task<ApiResponse<bool>> AddEbookReviewAsync(EbookReview review)
        {
            var response = new ApiResponse<bool>();

            try
            {
                if (review == null)
                {
                    response.Success = false;
                    response.Message = "Review cannot be null.";
                    return response;
                }

                var isAdded = await _reviewRepository.AddEbookReviewAsync(review);
                if (!isAdded)
                {
                    response.Success = false;
                    response.Message = "Failed to add the review.";
                    return response;
                }

                response.Success = true;
                response.Message = "Review added successfully.";
                response.Data = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while adding the review.";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        // Add Audiobook Review
        public async Task<ApiResponse<bool>> AddAudiobookReviewAsync(AudiobookReview review)
        {
            var response = new ApiResponse<bool>();

            try
            {
                if (review == null)
                {
                    response.Success = false;
                    response.Message = "Review cannot be null.";
                    return response;
                }

                var isAdded = await _reviewRepository.AddAudiobookReviewAsync(review);
                if (!isAdded)
                {
                    response.Success = false;
                    response.Message = "Failed to add the review.";
                    return response;
                }

                response.Success = true;
                response.Message = "Review added successfully.";
                response.Data = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while adding the review.";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        // Get Reviews for Normal Book
        public async Task<ApiResponse<List<ReviewDto>>> GetNormalBookReviewsAsync(int bookId)
        {
            var response = new ApiResponse<List<ReviewDto>>();
            var reviewdata = new List<ReviewDto>(); 

            try
            {
                var reviews = await _reviewRepository.GetNormalBookReviewsAsync(bookId);
                if (reviews == null || reviews.Count == 0)
                {
                    response.Success = false;
                    response.Message = "No reviews found for this NormalBook.";
                    return response;
                }

                foreach (var review in reviews)
                {
                    var Dto = new ReviewDto() {
                        Id = review.Id,
                        UserName=review.User.FullName,
                        UserProfil =review.User.ProfileImage,
                        ReviewText= review.ReviewText,
                        ReviewDate=review.ReviewDate,
                        Rating=review.Rating

                        
                    };
                    reviewdata.Add(Dto);

                }

                response.Success = true;
                response.Message = "Reviews fetched successfully.";
                response.Data = reviewdata;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while fetching the reviews.";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        // Get Reviews for Ebook
        public async Task<ApiResponse<List<ReviewDto>>> GetEbookReviewsAsync(int bookId)
        {
            var response = new ApiResponse<List<ReviewDto>>();
            var reviewdata = new List<ReviewDto>();
            try
            {
                var reviews = await _reviewRepository.GetEbookReviewsAsync(bookId);
                if (reviews == null || reviews.Count == 0)
                {
                    response.Success = false;
                    response.Message = "No reviews found for this Ebook.";
                    return response;
                }

                foreach (var review in reviews)
                {
                    var Dto = new ReviewDto()
                    {
                        Id = review.Id,
                        UserName = review.User.FullName,
                        UserProfil = review.User.ProfileImage,
                        ReviewText = review.ReviewText,
                        ReviewDate = review.ReviewDate,
                        Rating = review.Rating


                    };
                    reviewdata.Add(Dto);

                }

                response.Success = true;
                response.Message = "Reviews fetched successfully.";
                response.Data = reviewdata;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while fetching the reviews.";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

        // Get Reviews for Audiobook
        public async Task<ApiResponse<List<ReviewDto>>> GetAudiobookReviewsAsync(int bookId)
        {
            var response = new ApiResponse<List<ReviewDto>>();
            var reviewdata = new List<ReviewDto>();
            try
            {
                var reviews = await _reviewRepository.GetAudiobookReviewsAsync(bookId);
                if (reviews == null || reviews.Count == 0)
                {
                    response.Success = false;
                    response.Message = "No reviews found for this Audiobook.";
                    return response;
                }

                foreach (var review in reviews)
                {
                    var Dto = new ReviewDto()
                    {
                        Id = review.Id,
                        UserName = review.User.FullName,
                        UserProfil = review.User.ProfileImage,
                        ReviewText = review.ReviewText,
                        ReviewDate = review.ReviewDate,
                        Rating = review.Rating


                    };
                    reviewdata.Add(Dto);

                }

                response.Success = true;
                response.Message = "Reviews fetched successfully.";
                response.Data = reviewdata;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while fetching the reviews.";
                response.Errors.Add(ex.Message);
            }

            return response;
        }

    }

}